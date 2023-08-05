using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class ProductInOutRepository : Repository<ProductInOut>, IProductInOutRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ProductInOutRepository(AppDbContext context) : base(context)
        {
        }
    }
}
