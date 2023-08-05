using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class BlockSubcontractorBusinessGroupRepository : Repository<BlockSubcontractorBusinessGroup>, IBlockSubcontractorBusinessGroupRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public BlockSubcontractorBusinessGroupRepository(AppDbContext context) : base(context)
        {
        }

    }
}
