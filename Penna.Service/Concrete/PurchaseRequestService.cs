using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class PurchaseRequestService : BaseService<PurchaseRequest>, IPurchaseRequestService
    {
        public PurchaseRequestService(IUnitOfWork unitOfWork, IRepository<PurchaseRequest> repository) : base(unitOfWork, repository)
        {
        }
    }
}
