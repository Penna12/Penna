using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Penna.Data.Interfaces;
using Penna.Core.Extensions;
using Penna.Core.Utilities.Enums;
using System.Linq;
using System;

namespace Penna.Business.Concrete
{
    public class CurrentAccountService : BaseService<CurrentAccount>, ICurrentAccountService
    {
        public CurrentAccountService(IUnitOfWork unitOfWork, IRepository<CurrentAccount> repository) : base(unitOfWork, repository)
        {

        }

        public async Task<CurrentAccount> GetWithTenantByIdAsync(int productId)
        {
            return await _unitOfWork.CurrentAccount.GetWithTenantByIdAsync(productId);
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


        public IEnumerable<SelectListItem> GetCurrentAccountTypeListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(CurrentAccountTypeEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }

        public IEnumerable<SelectListItem> GetBusinessGroupListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(BusinessGroupEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }

        public IEnumerable<SelectListItem> GetCompanyStatusListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(StatusEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }
    }
}
