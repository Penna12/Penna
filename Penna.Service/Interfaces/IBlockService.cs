using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IBlockService : IService<Block>
    {
        Task<Block> GetWithProjectByIdAsync(int blockId);
        IEnumerable<SelectListItem> GetBlockListForDropDown(int projectId, int? selectedId = null);
        IEnumerable<SelectListItem> GetBlockTypeListForDropDown(string selectedId = null);
        IEnumerable<SelectListItem> GetCostCalculationListForDropDown(string selectedId = null);
        IEnumerable<SelectListItem> GetUserListForDropDown(int tenantId, string selectedId = null);
        IEnumerable<SelectListItem> GetSubcontractorListForDropDown(int tenantId, int? selectedId = null);
        IEnumerable<SelectListItem> GetUserPositionListForDropDown(int tenantId);
        Task<AppUser> GetAppUserByIdAsync(string userId);
        Task<CurrentAccount> GetCurrentAccountByIdAsync(int accountId);
        Task<IEnumerable<BlockTeam>> GetBlockTeamListAsync(int blockId);
    }
}
