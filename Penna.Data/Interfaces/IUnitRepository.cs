using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Data.Interfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        IEnumerable<SelectListItem> GetUnitListForDropDown(int? selectedId = null);
    }
}
