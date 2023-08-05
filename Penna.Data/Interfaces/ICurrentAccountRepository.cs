using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Utilities.Enums;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface ICurrentAccountRepository : IRepository<CurrentAccount>
    {
        Task<CurrentAccount> GetWithTenantByIdAsync(int currentAccountId);
        IEnumerable<SelectListItem> GetCurrentAccountListForDropDown(int tenantId, CurrentAccountTypeEnum? accountTypeId = null, int? selectedId = null);
    }
}
