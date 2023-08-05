using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Business.Interfaces
{
    public interface IAuthorityService : IService<Authority>
    {
        IEnumerable<SelectListItem> GetAuthorityListForDropDown(int? selectedId = null);
    }
}
