using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Penna.Data.Interfaces;
namespace Penna.Business.Concrete
{
    public class LaborService : BaseService<Labor>, ILaborService
    {
        public LaborService(IUnitOfWork unitOfWork, IRepository<Labor> repository) : base(unitOfWork, repository)
        {

        }

        public IEnumerable<SelectListItem> GetLaborListForDropDown(int tenantId, int? selectedId = null)
        {
            return _unitOfWork.Labor.GetLaborListForDropDown(tenantId, selectedId);
        }

        public async Task<Labor> GetWithTenantByIdAsync(int laborId)
        {
            return await _unitOfWork.Labor.GetWithTenantByIdAsync(laborId);
        }
    }
}
