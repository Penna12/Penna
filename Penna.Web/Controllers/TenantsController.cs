using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Constants;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class TenantsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TenantsController> _logger;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly ITownService _townService;
        private readonly ITenantService _tenantService;
        private readonly IAccountService _accountService;

        public TenantsController(ILogger<TenantsController> logger,
            ICountryService countryService, ICityService cityService, ITownService townService,
            ITenantService tenantService, IMapper mapper, IAccountService accountService)
        {
            _logger = logger;
            _countryService = countryService;
            _cityService = cityService;
            _townService = townService;
            _tenantService = tenantService;
            _mapper = mapper;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "TenantList";
            TenantAddDto tenantDto = new TenantAddDto();
            tenantDto.CountryList = _countryService.GetCountryListForDropDown();
            tenantDto.CityList = _cityService.GetCityListForDropDown(222);
            tenantDto.TownList = _townService.GetTownListForDropDown(40);

            return View(tenantDto);
        }

        
        #region Calling Json data
        [HttpPost]
        public async Task<IActionResult> Create(TenantAddDto tenantAddDto)
        {
            if (ModelState.IsValid)
            {
                var tenant = _mapper.Map<Tenant>(tenantAddDto);
                tenant.CountryDialCode = await _countryService.GetCountryDialCodeAsync(tenantAddDto.CountryId);
                tenant.CreatedBy = User.FindFirstValue("UserFullName"); 
                tenant.CreatedDate = DateTime.Now;
                tenant.IsActive = true;
                tenant.IsLocked = false;
                await _tenantService.AddAsync(tenant);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(TenantEditDto tenantEditDto)
        {
            if (ModelState.IsValid)
            {
                var tenant = _mapper.Map<Tenant>(tenantEditDto);
                tenant.UpdatedBy = User.FindFirstValue("UserFullName");
                tenant.UpdatedDate = DateTime.Now;
                _tenantService.Update(tenant);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tenant = await _tenantService.GetByIdAsync(id);
            if (tenant == null)
            {
                return Json(new { success = false });
            }
            _tenantService.Remove(tenant);
            return Json(new { success = true });
        }

        public async Task<IActionResult> GetAllData()
        {
            var list = await _tenantService.GetAllAsync();
            return Json(new { success = list != null, data = _mapper.Map<IEnumerable<TenantInfoDto>>(list) });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            // hiç bir yerde kullanılmadı
            var tenant = await _tenantService.GetByIdAsync(id);
            return Json(new { success = tenant != null, data = _mapper.Map<TenantEditDto>(tenant) });
        }


        [HttpGet]
        public IActionResult GetTenantDetailViewComponent(int id)
        {
            return ViewComponent("TenantDetail", new { id = id });
        }

        [HttpGet]
        public IActionResult GetTenantEditViewComponent(int id)
        {
            return ViewComponent("TenantEdit", new { id = id });
        }

        [HttpGet]
        public IActionResult FindAdminUserByTenantId(int tenantId)
        {
            var user = _accountService.GetUserInfoByTenantIdAndHasRoleAsync(tenantId, SD.ROLE_Admin);
            return Json(new { found = (user != null) });
        }

        [HttpGet]
        public IActionResult GetAdminUserByTenantId(int tenantId)
        {
            var user = _accountService.GetUserInfoByTenantIdAndHasRoleAsync(tenantId, SD.ROLE_Admin);
            if (user == null)
                return Json(new { found = false });
            return ViewComponent("AdminUser", new { id = tenantId });
        }


        [HttpPost]
        public async Task<IActionResult> SaveAdminUser(SignUpUserModel newAdminUser)
        {
            string errStr = string.Empty;
            newAdminUser.Role = SD.ROLE_Admin;
            var result = await _accountService.SignUpUserAsync(newAdminUser);
            if (result.Succeeded)
                return Json(new { success = true });
            else 
            {
                foreach (var item in result.Errors)
                {
                    if (item.Code.Contains("DublicateEmail"))
                    {
                        errStr += "Bu email adresi kullanılmaktadır. ";
                    }
                    if (item.Code.Contains("DublicateUserName"))
                    {
                        errStr += "Bu kullanıcı adı kullanılmaktadır. ";
                    }
                    if (item.Code.Contains("InvalidEmail"))
                    {
                        errStr += "Geçersiz eposta adresi. ";
                    }
                    if (item.Code.Contains("InvalidUserName"))
                    {
                        errStr += "Geçersiz kullanıcı adı. ";
                    } 
                }
            }
            return Json(new { success = false, message = errStr });
        }

        #endregion Calling Json data
    }
}
