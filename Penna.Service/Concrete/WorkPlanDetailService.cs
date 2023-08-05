using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class WorkPlanDetailDetailService : BaseService<WorkPlanDetail>, IWorkPlanDetailService
    {
        public WorkPlanDetailDetailService(IUnitOfWork unitOfWork, IRepository<WorkPlanDetail> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<WorkPlanDetail> GetWithSubDataByIdAsync(int workPlanDetailId)
        {
            return await _unitOfWork.WorkPlanDetail.GetWithSubDataByIdAsync(workPlanDetailId);
        }

        public IEnumerable<WorkPlanDetail> GetAllWithSubDataByWorkPlanIdAsync(int workPlanId)
        {
            return _unitOfWork.WorkPlanDetail.GetAllWithSubDataByWorkPlanIdAsync(workPlanId);
        }
    }
}
