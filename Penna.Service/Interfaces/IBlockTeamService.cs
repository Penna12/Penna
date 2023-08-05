using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IBlockTeamService : IService<BlockTeam>
    {
        Task<BlockTeam> GetWithBlockByIdAsync(int blockTeamId);
        Task<IEnumerable<BlockTeam>> GetBlockTeamListAsync(int blockId);
    }
}
