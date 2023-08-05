using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IBlockSubcontractorService : IService<BlockSubcontractor>
    {
        Task<BlockSubcontractor> GetWithBlockAndCurrentAccountByIdAsync(int blockSubcontractorId);
        IEnumerable<SelectListItem> GetBusinessGroupListForDropDown(string selectedId = null);
    }
}
