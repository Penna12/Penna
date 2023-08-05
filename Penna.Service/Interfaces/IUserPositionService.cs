using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Business.Interfaces
{
    public interface IUserPositionService : IService<UserPosition>
    {
        IEnumerable<SelectListItem> GetUserPositionListForDropDown(int? selectedId = null);
    }
}
