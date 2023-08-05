using Penna.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class ConcreteRequest : BaseModel, IEntity
    {
        public ConcreteRequest()
        {
            ConcreteCastings = new Collection<ConcreteCasting>();
        }
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        public DateTime RequestedDate { get; set; }
        public int BlockId { get; set; }
        public string Description { get; set; }

        public virtual Block Block { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ConcreteCasting> ConcreteCastings { get; set; }
    }
}
