using Penna.Core.Entities;
using System;

namespace Penna.Entities.Models
{
    public class ProductInOut : BaseModel, IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string DispatchPoint { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int? PurchaseId { get; set; }

        public virtual Product Product { get; set; }
    }
}
