using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IWorkPlanDetailRepository : IRepository<WorkPlanDetail>
    {
        Task<WorkPlanDetail> GetWithSubDataByIdAsync(int workPlanDetailId);
        IEnumerable<WorkPlanDetail> GetAllWithSubDataByWorkPlanIdAsync(int workPlanId);
    }
}
