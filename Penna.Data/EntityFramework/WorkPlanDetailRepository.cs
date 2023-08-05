using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Data.EntityFramework
{
    public class WorkPlanDetailRepository : Repository<WorkPlanDetail>, IWorkPlanDetailRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public WorkPlanDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<WorkPlanDetail> GetWithSubDataByIdAsync(int workPlanDetailId)
        {
            return await appDbContext.WorkPlanDetails
                .Include(x => x.WorkPlan)
                .Include(x => x.Unit)
                .Include(x => x.ParentWorkPlanDetail)
                .SingleOrDefaultAsync(x => x.Id == workPlanDetailId);
        }

        public IEnumerable<WorkPlanDetail> GetAllWithSubDataByWorkPlanIdAsync(int workPlanId)
        {
            return appDbContext.WorkPlanDetails
                .Include(x => x.WorkPlan)
                .Include(x => x.Unit)
                .Include(x => x.ParentWorkPlanDetail)
                .Where(x => x.WorkPlanId == workPlanId);
        }

    }
}