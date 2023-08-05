using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IProjectFileService : IService<ProjectFile>
    {
        Task<ProjectFile> GetWithProjectBlockApartmentByIdAsync(int projectFileId);
        IEnumerable<SelectListItem> GetFileTypeListForDropDown(string selectedId = null);
        
    }
}
