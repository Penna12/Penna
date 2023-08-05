using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using Penna.Data.Interfaces;
using Penna.Core.Utilities.Constants;

namespace Penna.Business.Concrete
{
    public class AuthorityGroupService : BaseService<AuthorityGroup>, IAuthorityGroupService
    {
        public AuthorityGroupService(IUnitOfWork unitOfWork, IRepository<AuthorityGroup> repository) : base(unitOfWork, repository)
        {

        }
        public IEnumerable<SelectListItem> GetAuthorityGroupListForDropDown(int? selectedId = null)
        {
            return _unitOfWork.AuthorityGroup.GetAuthorityGroupListForDropDown(SD.TenantId, selectedId);
        }
    }
}
