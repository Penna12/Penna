using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Data.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        IEnumerable<SelectListItem> GetCountryListForDropDown(int? selectedId = null);
    }
}
