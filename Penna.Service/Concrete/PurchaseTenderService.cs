using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class PurchaseTenderService : BaseService<PurchaseTender>, IPurchaseTenderService
    {
        public PurchaseTenderService(IUnitOfWork unitOfWork, IRepository<PurchaseTender> repository) : base(unitOfWork, repository)
        {
        }
    }
}
