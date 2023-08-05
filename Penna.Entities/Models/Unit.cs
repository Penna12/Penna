using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Unit : IEntity
    {
        public Unit()
        {
            Products = new Collection<Product>();
            WorkPlans = new Collection<WorkPlan>();
            WorkPlanDetails = new Collection<WorkPlanDetail>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<WorkPlan> WorkPlans { get; set; }
        public virtual ICollection<WorkPlanDetail> WorkPlanDetails { get; set; }
    }
}
