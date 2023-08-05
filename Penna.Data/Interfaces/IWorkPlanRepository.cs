using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IWorkPlanRepository : IRepository<WorkPlan>
    {
        Task<WorkPlan> GetWithSubDataByIdAsync(int workPlanId);
        IEnumerable<WorkPlan> GetAllWithSubDataByContractorIdAsync(int contractorId);
    }
}
