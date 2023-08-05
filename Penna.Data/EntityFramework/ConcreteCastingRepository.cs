using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class ConcreteCastingRepository : Repository<ConcreteCasting>, IConcreteCastingRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ConcreteCastingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
