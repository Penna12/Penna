using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Data.Interfaces
{
    public interface IAuthorityGroupRepository : IRepository<AuthorityGroup>
    {
        IEnumerable<SelectListItem> GetAuthorityGroupListForDropDown(int tenantId, int? selectedId = null);
    }
}
