using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class CurrentAccountDetailRepository : Repository<CurrentAccountDetail>, ICurrentAccountDetailRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public CurrentAccountDetailRepository(AppDbContext context) : base(context)
        {

        }
    }
}
