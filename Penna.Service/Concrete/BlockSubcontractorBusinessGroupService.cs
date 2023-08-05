using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class BlockSubcontractorBusinessGroupService : BaseService<BlockSubcontractorBusinessGroup>, IBlockSubcontractorBusinessGroupService
    {
        public BlockSubcontractorBusinessGroupService(IUnitOfWork unitOfWork, IRepository<BlockSubcontractorBusinessGroup> repository) : base(unitOfWork, repository)
        {

        }


    }
}
