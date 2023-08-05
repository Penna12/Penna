using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Enums;
using Penna.Entities.DTOs;
using System;
using System.Threading.Tasks;
using Penna.Core.Extensions;
using System.Security.Claims;
using Penna.Business.Filters;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class SubcontractorController : Controller
    {
        private readonly ILogger<SubcontractorController> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentAccountService _currentAccountService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly ITownService _townService;

        public SubcontractorController(ICurrentAccountService currentAccountService, 
            IMapper mapper, ILogger<SubcontractorController> logger, 
            ICountryService countryService, ICityService cityService, ITownService townService)
        {
            _currentAccountService = currentAccountService;
            _mapper = mapper;
            _logger = logger;
            _countryService = countryService;
            _cityService = cityService;
            _townService = townService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "SubcontractorList";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Taşeronlar" };
            Toolbar.Urls = new[] { "/", "#" };
            CurrentAccountDto currentAccountDto = new CurrentAccountDto();
            currentAccountDto.CountryList = _countryService.GetCountryListForDropDown();

            return View(currentAccountDto);
        }

        #region Calling Json data
        [HttpGet]
        public IActionResult GetEnumName(Enum name)
        {
            return Json(new { success = true, data = EnumExtensions.GetDisplayName(name) });
        }
        [HttpGet]
        public IActionResult GetBusinessGroupEnumList()
        {
            var list = EnumExtensions.GetAttributeList(typeof(BusinessGroupEnum));            
            return Json(list);
        }
        public async Task<IActionResult> GetAllData()
        {
            var list = await _currentAccountService.Where(c => c.AccountTypeId == CurrentAccountTypeEnum.Subcontractor);
            return Json(new { success = list != null, data = list });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            // hiç bir yerde kullanılmadı
            var subcontractor = await _currentAccountService.GetByIdAsync(id);
            var citydata = _cityService.GetCityListForDropDown(subcontractor.CountryId, subcontractor.CityId);
            var towndata = _townService.GetTownListForDropDown(subcontractor.CityId, subcontractor.TownId);
            return Json(new { success = subcontractor != null, data = subcontractor, citydata, towndata });
        }


        [HttpPost]
        public async Task<IActionResult> Create(CurrentAccountDto currentAccountAddDto)
        {
            if (ModelState.IsValid)
            {
                currentAccountAddDto.CurrentAccount.TenantId = Convert.ToInt32(User.FindFirst("TenantId")?.Value);
                currentAccountAddDto.CurrentAccount.AccountTypeId = CurrentAccountTypeEnum.Subcontractor;
                //currentAccountAddDto.CurrentAccount.CompanyStatusId = CompanyStatusEnum.WhiteList;
                currentAccountAddDto.CurrentAccount.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                currentAccountAddDto.CurrentAccount.CreatedDate = DateTime.Now;
                await _currentAccountService.AddAsync(currentAccountAddDto.CurrentAccount);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(CurrentAccountDto currentAccountEditDto)
        {
            if (ModelState.IsValid)
            {
                currentAccountEditDto.CurrentAccount.TenantId = Convert.ToInt32(User.FindFirst("TenantId")?.Value);
                currentAccountEditDto.CurrentAccount.AccountTypeId = CurrentAccountTypeEnum.Subcontractor;
                //currentAccountEditDto.CurrentAccount.CompanyStatusId = CompanyStatusEnum.WhiteList;
                currentAccountEditDto.CurrentAccount.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                currentAccountEditDto.CurrentAccount.UpdatedDate = DateTime.Now;
                _currentAccountService.Update(currentAccountEditDto.CurrentAccount);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var subcontractor = await _currentAccountService.GetByIdAsync(id);
            if (subcontractor == null)
            {
                return Json(new { success = false });
            }
            _currentAccountService.Remove(subcontractor);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult GetCityData(int cid)
        {
            return Json(new { data = _cityService.GetCityListForDropDown(cid) });
        }

        [HttpGet]
        public IActionResult GetTownData(int cid)
        {
            return Json(new { data = _townService.GetTownListForDropDown(cid) });
        }

        #endregion Calling Json data
    }
}
