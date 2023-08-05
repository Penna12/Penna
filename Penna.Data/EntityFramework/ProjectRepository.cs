using Microsoft.EntityFrameworkCore;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Threading.Tasks;


namespace Penna.Data.EntityFramework
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Project> GetWithTenantByIdAsync(int projectId)
        {
            return await appDbContext.Projects
                .Include(x => x.Tenant)
                .Include(x => x.CurrentAccount)
                .Include(x => x.Country)
                .Include(x => x.City)
                .Include(x => x.Town)
                .SingleOrDefaultAsync(x => x.Id == projectId);

        }
    }
}
