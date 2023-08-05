using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface ILaborService : IService<Labor>
    {
        Task<Labor> GetWithTenantByIdAsync(int laborId);
        IEnumerable<SelectListItem> GetLaborListForDropDown(int tenantId, int? selectedId = null);
    }
}
