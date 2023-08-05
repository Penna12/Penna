using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Data.Interfaces
{
    public interface IUserPositionRepository : IRepository<UserPosition>
    {
        IEnumerable<SelectListItem> GetUserPositionListForDropDown(int tenantId, int? selectedId = null);
    }
}
