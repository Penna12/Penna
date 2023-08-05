using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class CityService : BaseService<City>, ICityService
    {
        public CityService(IUnitOfWork unitOfWork, IRepository<City> repository) : base(unitOfWork, repository)
        {
        }

        public IEnumerable<SelectListItem> GetCityListForDropDown(int countryId, int? selectedId = null)
        {
            return _unitOfWork.City.GetCityListForDropDown(countryId, selectedId);
        }

        public async Task<City> GetWithCountryByIdAsync(int cityId)
        {
            return await _unitOfWork.City.GetWithCountryByIdAsync(cityId);
        }
    }
}
