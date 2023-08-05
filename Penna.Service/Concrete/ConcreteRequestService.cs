using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class ConcreteRequestService : BaseService<ConcreteRequest>, IConcreteRequestService
    {
        public ConcreteRequestService(IUnitOfWork unitOfWork, IRepository<ConcreteRequest> repository) : base(unitOfWork, repository)
        {
        }
    }
}
