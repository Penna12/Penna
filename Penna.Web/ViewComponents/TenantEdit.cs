using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Interfaces;
using Penna.Entities.DTOs;
using System.Threading.Tasks;

namespace Penna.Web.ViewComponents
{
    public class TenantEdit : ViewComponent
    {
        private readonly ITenantService _tenantService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly ITownService _townService;
        private readonly IMapper _mapper;

        public TenantEdit(ITenantService tenantService, IMapper mapper, ICountryService countryService, ICityService cityService, ITownService townService)
        {
            _tenantService = tenantService;
            _mapper = mapper;
            _countryService = countryService;
            _cityService = cityService;
            _townService = townService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id = null)
        {
            if (id.HasValue)
            {
                var model = _mapper.Map<TenantEditDto>(await _tenantService.GetByIdWithCityAndCountryAsync((int)id));
                model.CountryList = _countryService.GetCountryListForDropDown();
                model.CityList = _cityService.GetCityListForDropDown(222);
                ViewData["TenantEditDto"] = model;
                return View(model);
            }
            return View(); 
        }
    }
}
