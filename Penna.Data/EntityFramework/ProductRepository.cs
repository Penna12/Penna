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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product> GetWithProjectByIdAsync(int productId)
        {
            return await appDbContext.Products
                .Include(x => x.Project)
                .SingleOrDefaultAsync(x => x.Id == productId);
        }

        public IEnumerable<SelectListItem> GetProductListForDropDown(int projectId, int? selectedId = null)
        {
            return appDbContext.Products.Where(x => x.ProjectId == projectId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }

        public IEnumerable<SelectListItem> GetConcreteListForDropDown(int projectId, int? selectedId = null)
        {
            return appDbContext.Products.Where(x => x.ProjectId == projectId && x.IsConcrete == true).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}
