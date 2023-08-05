using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class ConcreteCastingService : BaseService<ConcreteCasting>, IConcreteCastingService
    {
        public ConcreteCastingService(IUnitOfWork unitOfWork, IRepository<ConcreteCasting> repository) : base(unitOfWork, repository)
        {
        }
    }
}
