using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Penna.Data.EntityFramework
{
    public class FixtureRepository : Repository<Fixture>, IFixtureRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public FixtureRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetFixtureListForDropDown(int projectId, int? selectedId = null)
        {
            return appDbContext.Fixtures.Where(x => x.ProjectId == projectId).Select(x => new SelectListItem()
            {
                Text = x.ProductName,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}
