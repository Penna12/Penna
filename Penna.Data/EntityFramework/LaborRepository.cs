using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Data.EntityFramework
{
    public class LaborRepository : Repository<Labor>, ILaborRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public LaborRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Labor> GetWithTenantByIdAsync(int laborId)
        {
            return await appDbContext.Labors
                .Include(x => x.Tenant)
                .SingleOrDefaultAsync(x => x.Id == laborId);
        }

        public IEnumerable<SelectListItem> GetLaborListForDropDown(int tenantId, int? selectedId = null)
        {
            return appDbContext.Labors.Where(x => x.TenantId == tenantId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}
