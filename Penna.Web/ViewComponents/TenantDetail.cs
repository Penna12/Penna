using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Interfaces;
using Penna.Entities.DTOs;
using System.Threading.Tasks;

namespace Penna.Web.ViewComponents
{
    public class TenantDetail : ViewComponent
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public TenantDetail(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id = null)
        {
            if (id.HasValue)
            {
                var model = await _tenantService.GetByIdWithCityAndCountryAsync((int)id);
                ViewData["TenantInfoDto"] = _mapper.Map<TenantInfoDto>(model);
            }
            return View(); 
        }
    }
}
