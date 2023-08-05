using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Penna.Data.EntityFramework
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public UnitRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetUnitListForDropDown(int? selectedId = null)
        {
            return appDbContext.Units.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}
