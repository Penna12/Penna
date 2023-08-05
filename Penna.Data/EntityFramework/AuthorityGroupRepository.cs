using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Penna.Data.EntityFramework
{
    public class AuthorityGroupRepository : Repository<AuthorityGroup>, IAuthorityGroupRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public AuthorityGroupRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetAuthorityGroupListForDropDown(int tenantId, int? selectedId = null)
        {
            return appDbContext.AuthorityGroups.Where(x => x.TenantId == tenantId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}
