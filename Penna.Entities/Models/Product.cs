using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Product : BaseModel, IEntity
    {
        public Product()
        {
            ProductInOuts = new Collection<ProductInOut>();
            PurchaseRequests = new Collection<PurchaseRequest>();
            Purchases = new Collection<Purchase>();
            ConcreteRequests = new Collection<ConcreteRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int UnitId { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public string Thickness { get; set; }
        public string Species { get; set; }
        public BusinessGroupEnum BusinessGroupId { get; set; }
        public StatusEnum Status { get; set; }
        public int ProjectId { get; set; }
        public bool IsConcrete { get; set; }

        public virtual Project Project { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<ProductInOut> ProductInOuts { get; set; }
        public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<ConcreteRequest> ConcreteRequests { get; set; }
    }
}
