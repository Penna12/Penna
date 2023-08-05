using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> GetWithCountryByIdAsync(int cityId);
        IEnumerable<SelectListItem> GetCityListForDropDown(int countryId, int? selectedId = null);
    }
}
