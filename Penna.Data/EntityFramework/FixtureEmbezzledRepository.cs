using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class FixtureEmbezzledRepository : Repository<FixtureEmbezzled>, IFixtureEmbezzledRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public FixtureEmbezzledRepository(AppDbContext context) : base(context)
        {
        }
    }
}
