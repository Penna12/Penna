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
    public class BlockRepository : Repository<Block>, IBlockRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public BlockRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Block> GetWithProjectByIdAsync(int blockId)
        {
            return await appDbContext.Blocks
                .Include(x => x.Project)
                .SingleOrDefaultAsync(x => x.Id == blockId);
        }

        public IEnumerable<SelectListItem> GetBlockListForDropDown(int projectId, int? selectedId = null)
        {
            return appDbContext.Blocks.Where(x => x.ProjectId == projectId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
        
    }
}
