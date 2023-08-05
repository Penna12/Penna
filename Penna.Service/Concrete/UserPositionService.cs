using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using Penna.Data.Interfaces;
using Penna.Core.Utilities.Constants;

namespace Penna.Business.Concrete
{
    public class UserPositionService : BaseService<UserPosition>, IUserPositionService
    {
        public UserPositionService(IUnitOfWork unitOfWork, IRepository<UserPosition> repository) : base(unitOfWork, repository)
        {

        }
        public IEnumerable<SelectListItem> GetUserPositionListForDropDown(int? selectedId = null)
        {
            return _unitOfWork.UserPosition.GetUserPositionListForDropDown(SD.TenantId, selectedId);
        }
    }
}
