using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IBlockTeamRepository : IRepository<BlockTeam>
    {
        Task<BlockTeam> GetWithBlockByIdAsync(int id);
        IEnumerable<SelectListItem> GetBlockTeamListForDropDown(int blockId, int? selectedId = null);
        Task<IEnumerable<BlockTeam>> GetBlockTeamsListAsync(int blockId);
    }
}
