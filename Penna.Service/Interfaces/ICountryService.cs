using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface ICountryService : IService<Country>
    {
        IEnumerable<SelectListItem> GetCountryListForDropDown(int? selectedId = null);
        Task<string> GetCountryDialCodeAsync(int countryId);
    }
}
