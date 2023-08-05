using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework
{
    public class UserPositionAuthorityRepository : Repository<UserPositionAuthority>, IUserPositionAuthorityRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public UserPositionAuthorityRepository(AppDbContext context) : base(context)
        {
        }

        
    }
}
