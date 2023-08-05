using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Business.Interfaces
{
    public interface IAuthorityGroupService : IService<AuthorityGroup>
    {
        IEnumerable<SelectListItem> GetAuthorityGroupListForDropDown(int? selectedId = null);
    }
}
