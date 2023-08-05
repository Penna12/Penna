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
    public class BlockTeamRepository : Repository<BlockTeam>, IBlockTeamRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public BlockTeamRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<BlockTeam> GetWithBlockByIdAsync(int id)
        {
            return await appDbContext.BlockTeams
                .Include(x => x.Block)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<SelectListItem> GetBlockTeamListForDropDown(int blockId, int? selectedId = null)
        {
            return appDbContext.BlockTeams.Include(x => x.User).Where(x => x.BlockId == blockId).Select(x => new SelectListItem()
            {
                Text = x.User.FullName,
                Value = x.User.Id,
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }

        public async Task<IEnumerable<BlockTeam>> GetBlockTeamsListAsync(int blockId)
        {
            return await appDbContext.BlockTeams
                .Include(x => x.User)
                .Include(x => x.UserPosition)
                .Include(x => x.Block)
                .Where(x => x.BlockId == blockId)
                .ToListAsync();
        }
    }
}
