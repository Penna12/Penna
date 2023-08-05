using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class PurchaseTenderRepository : Repository<PurchaseTender>, IPurchaseTenderRepository
    {
        //private AppDbContext appDbContext { get => _context as AppDbContext; }
        public PurchaseTenderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
