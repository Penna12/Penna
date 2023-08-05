using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class CurrentAccountDetailService : BaseService<CurrentAccountDetail>, ICurrentAccountDetailService
    {
        public CurrentAccountDetailService(IUnitOfWork unitOfWork, IRepository<CurrentAccountDetail> repository) : base(unitOfWork, repository)
        {

        }
       
    }
}
