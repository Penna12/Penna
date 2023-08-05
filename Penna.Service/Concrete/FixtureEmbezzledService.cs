using Penna.Business.Interfaces;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class FixtureEmbezzledService : BaseService<FixtureEmbezzled>, IFixtureEmbezzledService
    {
        public FixtureEmbezzledService(IUnitOfWork unitOfWork, IRepository<FixtureEmbezzled> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<List<FixtureEmbezzled>> GetAllByFixtureIdAsync(int fixtureId)
        {
            return (List<FixtureEmbezzled>)await _unitOfWork.FixtureEmbezzled.Where(f => f.FixtureId == fixtureId, includeProperties:"AppUser");
        }

        public async Task<List<FixtureEmbezzled>> GetAllByUserIdAsync(string userId)
        {
            return (List<FixtureEmbezzled>)await _unitOfWork.FixtureEmbezzled.Where(f => f.AppUserId == userId);
        }

        public Task<FixtureEmbezzled> GetWithFixtureByIdAsync(int id)
        {
            return _unitOfWork.FixtureEmbezzled.SingleOrDefaultAsync(f => f.Id == id, includeProperties:"Fixture");
        }
    }
}
