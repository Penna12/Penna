using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class PurchaseService : BaseService<Purchase>, IPurchaseService
    {
        public PurchaseService(IUnitOfWork unitOfWork, IRepository<Purchase> repository) : base(unitOfWork, repository)
        {
        }
    }
}
