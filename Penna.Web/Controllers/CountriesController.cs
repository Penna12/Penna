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
    public class CountriesController : Controller
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;

        public CountriesController(ILogger<CountriesController> logger,
            ICountryService countryService, IMapper mapper)
        {
            _logger = logger;
            _countryService = countryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["active"] = "CountryList";
            return View();
        }

        #region Calling Json data
        public async Task<IActionResult> GetAllData()
        {
            var list = await _countryService.GetAllAsync();
            return Json(new { success = list != null, data = _mapper.Map<IEnumerable<CountryViewDto>>(list) });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            // hiç bir yerde kullanılmadı
            var country = await _countryService.GetByIdAsync(id);
            return Json(new { success = country != null, data = _mapper.Map<CountryViewDto>(country) });
        }


        [HttpPost]
        public async Task<IActionResult> Create(CountryAddDto countryAddDto)
        {
            if (ModelState.IsValid)
            {
                await _countryService.AddAsync(_mapper.Map<Country>(countryAddDto));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(CountryEditDto countryEditDto)
        {
            if (ModelState.IsValid)
            {
                _countryService.Update(_mapper.Map<Country>(countryEditDto));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var country = await _countryService.GetByIdAsync(id);
            if (country == null)
            {
                return Json(new { success = false });
            }
            _countryService.Remove(country);
            return Json(new { success = true });
        }

        #endregion Calling Json data
    }
}
