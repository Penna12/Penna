using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class WorkPlanService : BaseService<WorkPlan>, IWorkPlanService
    {
        public WorkPlanService(IUnitOfWork unitOfWork, IRepository<WorkPlan> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<WorkPlan> GetWithSubDataByIdAsync(int workPlanId)
        {
            return await _unitOfWork.WorkPlan.GetWithSubDataByIdAsync(workPlanId);
        }

        public IEnumerable<WorkPlan> GetAllWithSubDataByContractorIdAsync(int contractorId)
        {
            return _unitOfWork.WorkPlan.GetAllWithSubDataByContractorIdAsync(contractorId);
        }
    }
}
