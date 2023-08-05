using Microsoft.AspNetCore.Mvc;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Constants;
using Penna.Entities.DTOs;

namespace Penna.Web.ViewComponents
{
    public class AdminUser : ViewComponent
    {
        private readonly IAccountService _accountService;

        public AdminUser(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IViewComponentResult Invoke(int id)
        {
            AppUserInfoDto appUserDto = _accountService.GetUserInfoByTenantIdAndHasRoleAsync(id, SD.ROLE_Admin);
            ViewData["AppUserInfoDto"] = appUserDto;
            return View();
        }
    }
}
