using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        public CountryService(IUnitOfWork unitOfWork, IRepository<Country> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<string> GetCountryDialCodeAsync(int countryId)
        {
            var country = await _unitOfWork.Country.GetByIdAsync(countryId);
            return country?.DialCode;
        }

        public IEnumerable<SelectListItem> GetCountryListForDropDown(int? selectedId = null)
        {
            return _unitOfWork.Country.GetCountryListForDropDown(selectedId);
        }


    }
}
