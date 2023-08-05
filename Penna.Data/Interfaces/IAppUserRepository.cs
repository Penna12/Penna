using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Data.Interfaces
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        IEnumerable<SelectListItem> GetUserListForDropDown(int tenantId, string selectedId = null);
    }
}
