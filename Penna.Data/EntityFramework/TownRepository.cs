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
    public class TownRepository : Repository<Town>, ITownRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public TownRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Town> GetWithCityByIdAsync(int townId)
        {
            return await appDbContext.Towns
                .Include(x => x.City)
                .SingleOrDefaultAsync(x => x.Id == townId);
        }

        public IEnumerable<SelectListItem> GetTownListForDropDown(int cityId, int? selectedId = null)
        {
            return appDbContext.Towns.Where(x => x.CityId == cityId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}
