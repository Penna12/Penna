using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IWorkPlanService : IService<WorkPlan>
    {
        Task<WorkPlan> GetWithSubDataByIdAsync(int workPlanId);
        IEnumerable<WorkPlan> GetAllWithSubDataByContractorIdAsync(int contractorId);
    }
}
