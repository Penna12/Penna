using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class PurchaseRequestRepository : Repository<PurchaseRequest>, IPurchaseRequestRepository
    {
        //private AppDbContext appDbContext { get => _context as AppDbContext; }
        public PurchaseRequestRepository(AppDbContext context) : base(context)
        {
        }
    }
}
