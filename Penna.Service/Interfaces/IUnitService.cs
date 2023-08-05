using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Business.Interfaces
{
    public interface IUnitService : IService<Unit>
    {
        IEnumerable<SelectListItem> GetUnitListForDropDown(int? selectedId = null);
    }
}
