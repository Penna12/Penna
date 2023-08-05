using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class PurchaseRequest : BaseModel, IEntity
    {
        public PurchaseRequest()
        {
            Purchases = new Collection<Purchase>();
            Priority = PriorityEnum.Normal;
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public PriorityEnum Priority { get; set; }

        public virtual Project Project { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
