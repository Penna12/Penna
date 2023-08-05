using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class ConcreteRequestRepository : Repository<ConcreteRequest>, IConcreteRequestRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ConcreteRequestRepository(AppDbContext context) : base(context)
        {
        }
    }
}
