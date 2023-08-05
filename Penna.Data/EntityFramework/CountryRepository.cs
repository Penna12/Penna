using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;


namespace Penna.Data.EntityFramework
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public CountryRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetCountryListForDropDown(int? selectedId = null)
        {
            return appDbContext.Countries.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false 
            });
        }
    }
}
