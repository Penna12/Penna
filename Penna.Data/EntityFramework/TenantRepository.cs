using Microsoft.EntityFrameworkCore;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Threading.Tasks;


namespace Penna.Data.EntityFramework
{
    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public TenantRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<Tenant> GetWithProjectByIdAsync(int TenantId)
        {
            return await appDbContext.Tenants
                .Include(x => x.City)
                .Include(x => x.Country)
                .Include(x => x.Projects)
                .SingleOrDefaultAsync(x => x.Id == TenantId);
        }
    }
}
