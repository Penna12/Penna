using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class UserPositionAuthorityService : BaseService<UserPositionAuthority>, IUserPositionAuthorityService
    {
        public UserPositionAuthorityService(IUnitOfWork unitOfWork, IRepository<UserPositionAuthority> repository) : base(unitOfWork, repository)
        {

        }
    }
}
