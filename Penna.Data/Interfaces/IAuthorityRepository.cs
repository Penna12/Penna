using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Data.Interfaces
{
    public interface IAuthorityRepository : IRepository<Authority>
    {
        IEnumerable<SelectListItem> GetAuthorityListForDropDown(int tenantId, int? selectedId = null);
    }
}
