using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Business.Interfaces;
using Penna.Core.Extensions;
using Penna.Core.Utilities.Enums;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class ProjectFileService : BaseService<ProjectFile>, IProjectFileService
    {
        public ProjectFileService(IUnitOfWork unitOfWork, IRepository<ProjectFile> repository) : base(unitOfWork, repository)
        {

        }

        public async Task<ProjectFile> GetWithProjectBlockApartmentByIdAsync(int projectFileId)
        {
            return await _unitOfWork.ProjectFile.GetWithProjectBlockApartmentByIdAsync(projectFileId);
        }

        public IEnumerable<SelectListItem> GetFileTypeListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(FileTypeEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }
    }
}
