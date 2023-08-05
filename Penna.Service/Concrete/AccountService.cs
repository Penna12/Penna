using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Constants;
using Penna.Data.UnitOfWork;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IEmailService emailService,
            IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppUser> GetAppUserWithTenantByEmailAsync(string email)
        {
            return await _unitOfWork.AppUser.SingleOrDefaultAsync(u => u.Email == email, includeProperties:"Tenant");
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<AppUser> GetUserByCurrentAccountIdAsync(int curId)
        {
            return await _unitOfWork.AppUser.SingleOrDefaultAsync(u => u.CurrentAccountId.GetValueOrDefault() == curId);
        }

        public AppUserInfoDto GetUserInfoByTenantIdAndHasRoleAsync(int tenantId, string roleName)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@TenantId", tenantId, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@RoleName", roleName, DbType.String, ParameterDirection.Input);
            //parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            return _unitOfWork.SP_Call.QuerySingle<AppUserInfoDto>(SD.usp_GetUserInfoByTenantAndRole, parameter);
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserDto userDto)
        {
            var result = await _userManager.CreateAsync(userDto.AppUser, userDto.Password);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(userDto.Role))
                    await _userManager.AddToRoleAsync(userDto.AppUser, userDto.Role);
                await SendEmailTemporaryPasswordAsync(userDto.AppUser, userDto.Password);
            }
            return result;
        }

        public async Task<IdentityResult> SignUpUserAsync(SignUpUserModel userModel)
        {
            var user = new AppUser()
            {
                FullName = userModel.FullName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
                UserName = userModel.Email,
                TenantId = userModel.TenantId
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(userModel.Role))
                    await _userManager.AddToRoleAsync(user, userModel.Role);
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }

        public async Task<IdentityResult> UpdateAsync(AppUser user)
        {
            // picture upload olmalı burada veya picture upload yapan ayrı bir servis olmalı
            var objFromDb = await _userManager.FindByIdAsync(user.Id);
            objFromDb.Email = user.Email;
            objFromDb.PhoneNumber = user.PhoneNumber;
            objFromDb.FullName = user.FullName;
            objFromDb.Address = user.Address;
            objFromDb.CityId = user.CityId;
            objFromDb.CountryId = user.CountryId;
            objFromDb.CountryDialCode = user.CountryDialCode;
            objFromDb.TCIdentityNo = user.TCIdentityNo;
            objFromDb.Status = user.Status;
            objFromDb.TenantId = user.TenantId;
            objFromDb.PictureUrl = user.PictureUrl;
            objFromDb.PictureRealName = user.PictureRealName;
            objFromDb.PictureContentType = user.PictureContentType;
            return await _userManager.UpdateAsync(objFromDb);
        }

        public async Task GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }

        public async Task GenerateForgotPasswordTokenAsync(AppUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            return await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, true);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task SignInAsync(AppUser user)
        {
            await _signInManager.SignInAsync(user, true);
        }

        public async Task AddClaimAsync(AppUser user, Claim claim)
        {
            await _userManager.AddClaimAsync(user, claim);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);
        }

        public async Task RemoveClaimAsync(AppUser user, Claim claim)
        {
            await _userManager.RemoveClaimAsync(user, claim);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);
        }

        public async Task<Claim> GetClaimAsync(AppUser user, string type)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.FirstOrDefault(x => x.Type == type);
        }

        public async Task<IList<Claim>> GetClaimsAsync(AppUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<bool> UserHasClaimAsync(AppUser user, string type)
        {
            var existingClaim = await _userManager.GetClaimsAsync(user);
            return (existingClaim.Any(x => x.Type == "ProjectId"));
        }

        public async Task RemoveExistsAndAddNewProjectClaimAsync(AppUser user, string projeId, string projeName, string tenantName)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            // Adding Claim for ProjectId 
            var existingClaim = claims.FirstOrDefault(x => x.Type == "ProjectId");
            if (existingClaim != null)
                await _userManager.RemoveClaimAsync(user, existingClaim);
            var newClaim = new Claim("ProjectId", projeId);
            await _userManager.AddClaimAsync(user, newClaim);
            // Adding Claim for ProjectName
            existingClaim = claims.FirstOrDefault(x => x.Type == "ProjectName");
            if (existingClaim != null)
                await _userManager.RemoveClaimAsync(user, existingClaim);
            newClaim = new Claim("ProjectName", projeName);
            await _userManager.AddClaimAsync(user, newClaim);
            // Adding Claim for TenantName
            existingClaim = claims.FirstOrDefault(x => x.Type == "TenantName");
            if (existingClaim != null)
                await _userManager.RemoveClaimAsync(user, existingClaim);
            newClaim = new Claim("TenantName", tenantName);
            await _userManager.AddClaimAsync(user, newClaim);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }


        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }

        private async Task SendEmailConfirmationEmail(AppUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FullName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForEmailConfirmation(options);
        }

        private async Task SendForgotPasswordEmail(AppUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FullName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForForgotPassword(options);
        }

        public async Task SendEmailTemporaryPasswordAsync(AppUser user, string password)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FullName),
                    new KeyValuePair<string, string>("{{Email}}", user.Email),
                    new KeyValuePair<string, string>("{{Password}}", password),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain))
                }
            };

            await _emailService.SendEmailForTemporaryPassword(options);
        }

        public async Task LockUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.LockoutEnd = DateTime.Now.AddYears(1000);
            await _userManager.UpdateAsync(user);
        }

        public async Task UnLockUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.LockoutEnd = DateTime.Now;
            await _userManager.UpdateAsync(user);
            throw new System.NotImplementedException();
        }


        public async Task SaveImageInfoAsync(string Id, ResponseImageDto response)
        {
            var objFromDb = await _userManager.FindByIdAsync(Id);
            objFromDb.PictureUrl = response.NewName;
            objFromDb.PictureRealName = response.RealName;
            objFromDb.PictureContentType = response.ContentType;
            await _userManager.UpdateAsync(objFromDb);
        }

        public async Task RefreshSignInAsync(AppUser user)
        {
            await _signInManager.RefreshSignInAsync(user);
        }


        public async Task UpdateNewPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await _userManager.ResetPasswordAsync(user, token, newPassword);
                await SendEmailTemporaryPasswordAsync(user, newPassword);
            }
        }
    }
}
