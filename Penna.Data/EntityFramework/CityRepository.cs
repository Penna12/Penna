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
    public class CityRepository : Repository<City>, ICityRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<City> GetWithCountryByIdAsync(int cityId)
        {
            return await appDbContext.Cities
                .Include(x => x.Country)
                .SingleOrDefaultAsync(x => x.Id == cityId);
        }

        public IEnumerable<SelectListItem> GetCityListForDropDown(int countryId, int? selectedId = null)
        {
            return appDbContext.Cities.Where(x => x.CountryId == countryId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}