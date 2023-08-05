using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        //private AppDbContext appDbContext { get => _context as AppDbContext; }
        public PurchaseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
