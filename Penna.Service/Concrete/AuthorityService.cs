using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using Penna.Data.Interfaces;
using Penna.Core.Utilities.Constants;

namespace Penna.Business.Concrete
{
    public class AuthorityService : BaseService<Authority>, IAuthorityService
    {
        public AuthorityService(IUnitOfWork unitOfWork, IRepository<Authority> repository) : base(unitOfWork, repository)
        {

        }
        public IEnumerable<SelectListItem> GetAuthorityListForDropDown(int? selectedId = null)
        {
            return _unitOfWork.Authority.GetAuthorityListForDropDown(SD.TenantId, selectedId);
        }
    }
}
