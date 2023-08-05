using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface ILaborRepository : IRepository<Labor>
    {
        Task<Labor> GetWithTenantByIdAsync(int laborId);
        IEnumerable<SelectListItem> GetLaborListForDropDown(int tenantId, int? selectedId = null);
    }
}
