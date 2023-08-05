using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Enums;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        public ProjectService(IUnitOfWork unitOfWork, IRepository<Project> repository) : base(unitOfWork, repository)
        {
        }
        public async Task<Project> GetWithTenantByProjectIdAsync(int projectId)
        {
            return await _unitOfWork.Project.GetWithTenantByIdAsync(projectId);
        }

        public IEnumerable<SelectListItem> GetCurrentAccountListForDropDown(int tenantId, CurrentAccountTypeEnum? accountTypeId = null, int? selectedId = null)
        {
            return _unitOfWork.CurrentAccount.GetCurrentAccountListForDropDown(tenantId, accountTypeId, selectedId);
        }

        public IEnumerable<SelectListItem> GetCountryListForDropDown(int? selectedId = null)
        {
            return _unitOfWork.Country.GetCountryListForDropDown(selectedId);
        }

        public IEnumerable<SelectListItem> GetCityListForDropDown(int countryId, int? selectedId = null)
        {
            return _unitOfWork.City.GetCityListForDropDown(countryId, selectedId);
        }

        public IEnumerable<SelectListItem> GetTownListForDropDown(int cityId, int? selectedId = null)
        {
            return _unitOfWork.Town.GetTownListForDropDown(cityId, selectedId);
        }
    }
}
