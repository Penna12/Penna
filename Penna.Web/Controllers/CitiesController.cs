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
    public class CitiesController : Controller
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public CitiesController(ILogger<CitiesController> logger,
            ICountryService countryService, ICityService cityService, IMapper mapper)
        {
            _logger = logger;
            _countryService = countryService;
            _cityService = cityService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["active"] = "CityList";
            CountrySelectListDto countryDto = new CountrySelectListDto();
            countryDto.CountryList = _countryService.GetCountryListForDropDown();
            return View(countryDto);
        }

        #region Calling Json data
        public async Task<IActionResult> GetAllData()
        {
            var list = await _cityService.GetAllAsync();
            return Json(new { success = list != null, data = _mapper.Map<IEnumerable<CityViewDto>>(list) });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            // hiç bir yerde kullanılmadı
            var city = await _cityService.GetByIdAsync(id);
            return Json(new { success = city != null, data = _mapper.Map<CityViewDto>(city) });
        }


        [HttpPost]
        public async Task<IActionResult> Create(CityAddDto cityAddDto)
        {
            if (ModelState.IsValid)
            {
                await _cityService.AddAsync(_mapper.Map<City>(cityAddDto));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(CityEditDto cityEditDto)
        {
            if (ModelState.IsValid)
            {
                _cityService.Update(_mapper.Map<City>(cityEditDto));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null)
            {
                return Json(new { success = false });
            }
            _cityService.Remove(city);
            return Json(new { success = true });
        }

        #endregion Calling Json data
    }
}
