using Microsoft.AspNetCore.Identity;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IAccountService
    {
        Task<AppUser> GetAppUserWithTenantByEmailAsync(string email);
        Task<AppUser> GetUserByNameAsync(string username);

        Task<AppUser> GetUserByEmailAsync(string email);

        Task<AppUser> GetUserByCurrentAccountIdAsync(int curId);
        AppUserInfoDto GetUserInfoByTenantIdAndHasRoleAsync(int tenantId, string roleName);

        Task<IdentityResult> CreateUserAsync(CreateUserDto userDto);
        Task<IdentityResult> SignUpUserAsync(SignUpUserModel userModel);

        Task<IdentityResult> UpdateAsync(AppUser user);

        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);

        Task SignOutAsync();
        Task SignInAsync(AppUser user);
        Task AddClaimAsync(AppUser user, Claim claim);
        Task RemoveClaimAsync(AppUser user, Claim claim);
        Task<Claim> GetClaimAsync(AppUser user, string type);
        Task<IList<Claim>> GetClaimsAsync(AppUser user);
        Task<bool> UserHasClaimAsync(AppUser user, string type);
        Task RemoveExistsAndAddNewProjectClaimAsync(AppUser user, string projeId, string projeName, string tenantName);

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);

        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);

        Task GenerateEmailConfirmationTokenAsync(AppUser user);

        Task GenerateForgotPasswordTokenAsync(AppUser user);

        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
        Task LockUserAsync(string userId);
        Task UnLockUserAsync(string userId);

        Task SaveImageInfoAsync(string Id, ResponseImageDto response);

        Task RefreshSignInAsync(AppUser user);

        Task UpdateNewPasswordAsync(string userId, string newPassword);
    }
}
