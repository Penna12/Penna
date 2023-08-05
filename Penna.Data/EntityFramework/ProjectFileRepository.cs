using Microsoft.EntityFrameworkCore;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Data.EntityFramework
{
    public class ProjectFileRepository : Repository<ProjectFile>, IProjectFileRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ProjectFileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ProjectFile> GetWithProjectBlockApartmentByIdAsync(int projectFileId)
        {
            return await appDbContext.ProjectFiles
                .Include(x => x.Project)
                .Include(x => x.Block)
                .Include(x => x.Apartment)
                .SingleOrDefaultAsync(x => x.Id == projectFileId);
        }
    }
}
