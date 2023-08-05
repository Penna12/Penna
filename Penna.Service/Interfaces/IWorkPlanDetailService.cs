using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IWorkPlanDetailService : IService<WorkPlanDetail>
    {
        Task<WorkPlanDetail> GetWithSubDataByIdAsync(int workPlanDetailId);
        IEnumerable<WorkPlanDetail> GetAllWithSubDataByWorkPlanIdAsync(int workPlanId);
    }
}
