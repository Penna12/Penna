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
    public class WorkPlanRepository : Repository<WorkPlan>, IWorkPlanRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public WorkPlanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<WorkPlan> GetWithSubDataByIdAsync(int workPlanId)
        {
            return await appDbContext.WorkPlans
                .Include(x => x.Unit)
                .Include(x => x.CurrentAccount)
                .Include(x => x.WorkPlanDetails)
                .SingleOrDefaultAsync(x => x.Id == workPlanId);
        }

        public IEnumerable<WorkPlan> GetAllWithSubDataByContractorIdAsync(int contractorId)
        {
            return appDbContext.WorkPlans
                .Include(x => x.Unit)
                .Include(x => x.CurrentAccount)
                .Include(x => x.WorkPlanDetails)
                .Where(x => x.ContractorCurrentAccountId == contractorId);
        }
    }
}