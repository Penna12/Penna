using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Penna.Core.Utilities.Enums;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Data.EntityFramework
{
    public class CurrentAccountRepository : Repository<CurrentAccount>, ICurrentAccountRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public CurrentAccountRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<CurrentAccount> GetWithTenantByIdAsync(int currentAccountId)
        {
            return await appDbContext.CurrentAccounts
                .Include(x => x.Tenant)
                .Include(x => x.CurrentAccountDetails)
                .SingleOrDefaultAsync(x => x.Id == currentAccountId);
        }

        public IEnumerable<SelectListItem> GetCurrentAccountListForDropDown(int tenantId, CurrentAccountTypeEnum? accountTypeId = null, int? selectedId = null)
        {
            return appDbContext.CurrentAccounts.Where(x => x.TenantId == tenantId && (accountTypeId == null || x.AccountTypeId == accountTypeId)).Select(x => new SelectListItem()
            {
                Text = x.CompanyName,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}
