using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Business.Concrete
{
    public class AppUserService : BaseService<AppUser>, IAppUserService
    {
        public AppUserService(IUnitOfWork unitOfWork, IRepository<AppUser> repository) : base(unitOfWork, repository)
        {
            
        }
        public IEnumerable<SelectListItem> GetUserListForDropDown(int tenantId, string selectedId = null)
        {
            return _unitOfWork.AppUser.GetUserListForDropDown(tenantId, selectedId);
        }
    }
}
