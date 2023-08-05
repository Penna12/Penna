using Penna.Core.Entities;
using System;

namespace Penna.Entities.Models
{
    public class WorkPlanDetail : BaseModel, IEntity
    {
        public int Id { get; set; }
        public int WorkPlanId { get; set; }
        public int? ParentWorkPlanDetailId { get; set; }
        public DateTime PlanDate { get; set; } = DateTime.Now;
        public int AssignedWorkAmount { get; set; }
        public int UnitId { get; set; }
        public int DoneWorkAmount { get; set; }
        public int RefuseWorkAmount { get; set; }
        public string Description { get; set; }

        public virtual WorkPlan WorkPlan { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual WorkPlanDetail ParentWorkPlanDetail { get; set; }
    }
}
