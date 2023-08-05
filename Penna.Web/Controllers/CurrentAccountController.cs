using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Extensions;
using Penna.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class CurrentAccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICurrentAccountService _currentAccountService;
        public CurrentAccountController(IMapper mapper, ICurrentAccountService currentAccountService)
        {
            _mapper = mapper;
            _currentAccountService = currentAccountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            TempData["active"] = "CurrentAccountDefine";
            Toolbar.Title = "Cari Hesap Oluştur";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Cari Hesap Tanımlama" };
            Toolbar.Urls = new[] { "/", "#" };

            CurrentAccountDto currentAccountDto = new CurrentAccountDto();
            if (id == null)
            {
                currentAccountDto.CountryList = _currentAccountService.GetCountryListForDropDown();
                currentAccountDto.CurrentAccount.TenantId = Convert.ToInt32(User.GetClaimValue("TenantId"));
                return View(currentAccountDto);
            }

            currentAccountDto.CurrentAccount = await _currentAccountService.SingleOrDefaultAsync(x => x.Id == id, includeProperties: "Tenant,Country,City,Town,Bank");
            if (currentAccountDto.CurrentAccount == null)
            {
                return NotFound();
            }
            currentAccountDto.CountryList = _currentAccountService.GetCountryListForDropDown(currentAccountDto.CurrentAccount.CountryId);
            currentAccountDto.CityList = _currentAccountService.GetCityListForDropDown(currentAccountDto.CurrentAccount.CountryId, currentAccountDto.CurrentAccount.CityId);
            currentAccountDto.TownList = _currentAccountService.GetTownListForDropDown(currentAccountDto.CurrentAccount.CityId, currentAccountDto.CurrentAccount.TownId);

            return View(currentAccountDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CurrentAccountDto currentAccountDto)
        {
            if (ModelState.IsValid)
            {
                if (currentAccountDto.CurrentAccount.Id == 0)
                {
                    currentAccountDto.CurrentAccount.TenantId = Convert.ToInt32(User.GetClaimValue("TenantId"));
                    currentAccountDto.CurrentAccount.CreatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                    currentAccountDto.CurrentAccount.CreatedDate = DateTime.Now;
                    await _currentAccountService.AddAsync(currentAccountDto.CurrentAccount);
                }
                else
                {
                    currentAccountDto.CurrentAccount.TenantId = Convert.ToInt32(User.GetClaimValue("TenantId"));
                    currentAccountDto.CurrentAccount.UpdatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                    currentAccountDto.CurrentAccount.UpdatedDate = DateTime.Now;
                    _currentAccountService.Update(currentAccountDto.CurrentAccount);
                }
                return RedirectToAction(nameof(Index));
            }
            currentAccountDto.CountryList = _currentAccountService.GetCountryListForDropDown(currentAccountDto.CurrentAccount.CountryId);
            currentAccountDto.CityList = _currentAccountService.GetCityListForDropDown(currentAccountDto.CurrentAccount.CountryId, currentAccountDto.CurrentAccount.CityId);
            currentAccountDto.TownList = _currentAccountService.GetTownListForDropDown(currentAccountDto.CurrentAccount.CityId, currentAccountDto.CurrentAccount.TownId);
            return View(currentAccountDto);
        }


    }
}
