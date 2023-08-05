using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class TownsController : Controller
    {
        private readonly ILogger<TownsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly ITownService _townService;

        public TownsController(ILogger<TownsController> logger, IMapper mapper,
            ICountryService countryService, ICityService cityService, ITownService townService)
        {
            _logger = logger;
            _mapper = mapper;
            _countryService = countryService;
            _cityService = cityService;
            _townService = townService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "TownList";
            CountryAndCitySelectListDto ddlDto = new CountryAndCitySelectListDto();
            ddlDto.CountryList = _countryService.GetCountryListForDropDown();
            ddlDto.CityList = _cityService.GetCityListForDropDown(222);
            return View(ddlDto);
        }

        #region Calling Json data
        public async Task<IActionResult> GetAllData()
        {
            var list = await _townService.GetAllAsync();
            return Json(new { success = list != null, data = _mapper.Map<IEnumerable<TownViewDto>>(list) });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            // hiç bir yerde kullanılmadı
            var town = await _townService.GetByIdAsync(id);
            return Json(new { success = town != null, data = _mapper.Map<TownViewDto>(town) });
        }


        [HttpPost]
        public async Task<IActionResult> Create(TownAddDto townAddDto)
        {
            if (ModelState.IsValid)
            {
                await _townService.AddAsync(_mapper.Map<Town>(townAddDto));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(TownEditDto townEditDto)
        {
            if (ModelState.IsValid)
            {
                _townService.Update(_mapper.Map<Town>(townEditDto));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var town = await _townService.GetByIdAsync(id);
            if (town == null)
            {
                return Json(new { success = false });
            }
            _townService.Remove(town);
            return Json(new { success = true });
        }

        #endregion Calling Json data
    }
}
