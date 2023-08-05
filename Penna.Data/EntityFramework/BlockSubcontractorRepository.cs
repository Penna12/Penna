using Microsoft.EntityFrameworkCore;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Data.EntityFramework
{
    public class BlockSubcontractorRepository : Repository<BlockSubcontractor>, IBlockSubcontractorRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public BlockSubcontractorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<BlockSubcontractor> GetWithBlockAndCurrentAccountByIdAsync(int blockSubcontractorId)
        {
            return await appDbContext.BlockSubcontractors
                .Include(x => x.Block)
                .Include(x => x.CurrentAccount)
                .SingleOrDefaultAsync(x => x.Id == blockSubcontractorId);
        }
    }
}
