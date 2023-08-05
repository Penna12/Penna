using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IBlockRepository : IRepository<Block>
    {
        Task<Block> GetWithProjectByIdAsync(int blockId);
        IEnumerable<SelectListItem> GetBlockListForDropDown(int projectId, int? selectedId = null);
    }
}
