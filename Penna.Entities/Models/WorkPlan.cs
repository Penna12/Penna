using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class WorkPlan : BaseModel, IEntity
    {
        public WorkPlan()
        {
            WorkPlanDetails = new Collection<WorkPlanDetail>();
        }

        public int Id { get; set; }
        public string WorkName { get; set; }
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public int Duration { get; set; } = 0;
        public BusinessGroupEnum BusinessGroup { get; set; }
        public int ContractorCurrentAccountId { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual CurrentAccount CurrentAccount { get; set; }
        public virtual ICollection<WorkPlanDetail> WorkPlanDetails { get; set; }
    }
}
