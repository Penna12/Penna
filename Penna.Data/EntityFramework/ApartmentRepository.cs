using Microsoft.EntityFrameworkCore;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Data.EntityFramework
{
    public class ApartmentRepository : Repository<Apartment>, IApartmentRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ApartmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Apartment> GetWithBlockAndCurrentAccountByIdAsync(int apartmentId)
        {
            return await appDbContext.Apartments
                .Include(x => x.Block)
                .Include(x => x.CurrentAccount)
                .SingleOrDefaultAsync(x => x.Id == apartmentId);
        }
    }
}
