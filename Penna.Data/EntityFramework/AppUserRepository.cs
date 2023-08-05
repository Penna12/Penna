using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Penna.Data.EntityFramework
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public AppUserRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetUserListForDropDown(int tenantId, string selectedId = null)
        {
            return appDbContext.AppUsers.Where(x => x.TenantId == tenantId).Select(x => new SelectListItem()
            {
                Text = x.FullName,
                Value = x.Id,
                Selected = (!string.IsNullOrEmpty(selectedId)) && selectedId == x.Id
            });
        }

    }
}
