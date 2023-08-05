using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Utilities.Enums;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface ICurrentAccountService : IService<CurrentAccount>
    {
        Task<CurrentAccount> GetWithTenantByIdAsync(int currentAccountId);
        IEnumerable<SelectListItem> GetCurrentAccountListForDropDown(int tenantId, CurrentAccountTypeEnum? accountTypeId = null, int? selectedId = null);
        
        IEnumerable<SelectListItem> GetCountryListForDropDown(int? selectedId = null);
        IEnumerable<SelectListItem> GetCityListForDropDown(int countryId, int? selectedId = null);
        IEnumerable<SelectListItem> GetTownListForDropDown(int cityId, int? selectedId = null);

        IEnumerable<SelectListItem> GetCurrentAccountTypeListForDropDown(string selectedId = null);
        IEnumerable<SelectListItem> GetBusinessGroupListForDropDown(string selectedId = null);
        IEnumerable<SelectListItem> GetCompanyStatusListForDropDown(string selectedId = null);
    }
}
