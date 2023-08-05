using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Utilities.Enums;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IProjectService : IService<Project>
    {
        Task<Project> GetWithTenantByProjectIdAsync(int projectId);
        IEnumerable<SelectListItem> GetCurrentAccountListForDropDown(int tenantId, CurrentAccountTypeEnum? accountTypeId = null, int? selectedId = null);
        IEnumerable<SelectListItem> GetCountryListForDropDown(int? selectedId = null);
        IEnumerable<SelectListItem> GetCityListForDropDown(int countryId, int? selectedId = null);
        IEnumerable<SelectListItem> GetTownListForDropDown(int cityId, int? selectedId = null);
    }
}
