using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Threading.Tasks;
using Penna.Data.Interfaces;
using System.Collections.Generic;

namespace Penna.Business.Concrete
{
    public class BlockTeamService : BaseService<BlockTeam>, IBlockTeamService
    {
        public BlockTeamService(IUnitOfWork unitOfWork, IRepository<BlockTeam> repository) : base(unitOfWork, repository)
        {

        }

        public async Task<BlockTeam> GetWithBlockByIdAsync(int blockTeamId)
        {
            return await _unitOfWork.BlockTeam.GetWithBlockByIdAsync(blockTeamId);
        }

        public async Task<IEnumerable<BlockTeam>> GetBlockTeamListAsync(int blockId)
        {
            return await _unitOfWork.BlockTeam.GetBlockTeamsListAsync(blockId);
        }
    }
}
