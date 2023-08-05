using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Business.Interfaces
{
    public interface IAppUserService : IService<AppUser>
    {
        IEnumerable<SelectListItem> GetUserListForDropDown(int tenantId, string selectedId = null);
    }
}
