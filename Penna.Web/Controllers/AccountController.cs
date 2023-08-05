using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Penna.Core.Utilities.Constants;
using Penna.Core.Extensions;

namespace Penna.Web.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IImageService _imageService;
        protected AppUser CurrentUser => _accountService.GetUserByNameAsync(User.Identity.Name).Result;

        public AccountController(IMapper mapper,
            ILogger<AccountController> logger,
            IAccountService accountService,
            IUserService userService,
            ICityService cityService,
            ICountryService countryService,
            IImageService imageService)
        {
            _mapper = mapper;
            _logger = logger;
            _accountService = accountService;
            _userService = userService;
            _cityService = cityService;
            _countryService = countryService;
            _imageService = imageService;
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            Toolbar.Set("Giriş Yap", "", new[] { "Hesaplar", "Giriş" }, new[] { "#", "#" });
            return View(new SignInModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
        {
            Toolbar.Set("Giriş Yap", "", new[] { "Hesaplar", "Giriş" }, new[] { "#", "#" });

            var result = await _accountService.PasswordSignInAsync(signInModel);
            if (result.Succeeded)
            {
                var appUser = await _accountService.GetAppUserWithTenantByEmailAsync(signInModel.Email);
                var tenant = appUser?.Tenant;
                SD.TenantId = tenant != null ? tenant.Id : 0;
                SD.TenantName = tenant != null ? tenant.FullName : string.Empty;
                SD.CurAccountId = appUser.CurrentAccountId.GetValueOrDefault();
                //SD.CurAccountName = appUser.CurrentAccount?.CompanyName;

                HttpContext.Session.SetObject(SD.SESSION_KEY_USER, appUser);
                HttpContext.Session.SetObject(SD.SESSION_KEY_TENANT, tenant);
                HttpContext.Session.SetString(SD.SESSION_KEY_TENANT_ID, SD.TenantId.ToString());
                HttpContext.Session.SetString(SD.SESSION_KEY_TENANT_NAME, SD.TenantName);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                return View(signInModel);
            }
        }


        public IActionResult LogOut()
        {
            SD.TenantId = 0;
            SD.TenantName = string.Empty;
            SD.ProjectId = 0;
            SD.BlockId = 0;
            HttpContext.Session.Clear();
            _accountService.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            Toolbar.Set("Register", "", new[] { "Hesaplar", "Register" }, new[] { "#", "#" });
            return View();
        }

        public IActionResult AccessDenied()
        {
            Toolbar.Set("Access Denied", "", new[] { "Hesaplar", "Access Denied" }, new[] { "#", "#" });
            return View();
        }

        [ServiceFilter(typeof(TenantNotFoundFilter))]
        public IActionResult GetById(int id)
        {
            Toolbar.Set("Arama", "", new[] { "Hesaplar", "Ara" }, new[] { "#", "#" });
            return View();
        }

        public async Task<IActionResult> MyProfile()
        {
            TempData["active"] = "MyProfile";
            Toolbar.Set("Profilim", "", new[] { "Hesaplar", "Profilim" }, new[] { "#", "#" });
            AppUserEditDto appUserDto = new AppUserEditDto();
            appUserDto = _mapper.Map<AppUserEditDto>(await _accountService.GetUserByNameAsync(_userService.GetUserName()));
            appUserDto.CityList = _cityService.GetCityListForDropDown(222);
            appUserDto.CountryList = _countryService.GetCountryListForDropDown();
            return View(appUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfile(AppUserEditDto appUserDto)
        {
            var result = await _accountService.UpdateAsync(_mapper.Map<AppUser>(appUserDto));
            return RedirectToAction(nameof(MyProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ProfileImageUpload(string Id, string PictureUrl)
        {
            var file = HttpContext.Request.Form.Files?.FirstOrDefault();
            if (file != null)
            {
                var response = await _imageService.UploadProfileImageAsync(file, PictureUrl);
                if (response != null)
                {
                    await _accountService.SaveImageInfoAsync(Id, response);

                    var user = await _accountService.GetUserByNameAsync(User.Identity.Name);
                    await _accountService.RefreshSignInAsync(user);
                }
            }
            return RedirectToAction(nameof(MyProfile));
        }

        public async Task<IActionResult> ChangePassword()
        {
            TempData["active"] = "ChangePassword";
            Toolbar.Set("Şifre Değiştir", "", new[] { "Hesaplar", "Şifre Değiştir" }, new[] { "#", "#" });
            ChangePasswordDto changePasswordDto = new ChangePasswordDto();
            changePasswordDto.AppUserInfo = _mapper.Map<AppUserInfoDto>(await _accountService.GetUserByNameAsync(_userService.GetUserName()));
            return View(changePasswordDto);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel ChangePassword)
        {
            TempData["active"] = "ChangePassword";
            Toolbar.Set("Şifre Değiştir", "", new[] { "Hesaplar", "Şifre Değiştir" }, new[] { "#", "#" });

           var result = await _accountService.ChangePasswordAsync(ChangePassword);
           if (result.Succeeded)
            {
                TempData["color"] = "success";
                TempData["title"] = "Başarılı işlem";
                TempData["message"] = "Şifreniz değiştirildi";
            } else
            {
                 TempData["color"] = "danger";
                 TempData["title"] = "Başarısız işlem";
                 TempData["message"] = "Şifreniz değiştirilemedi";
            }

            return RedirectToAction(nameof(ChangePassword));
        }

    }
}
