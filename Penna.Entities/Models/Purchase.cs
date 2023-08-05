using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Purchase : BaseModel, IEntity
    {
        public Purchase()
        {
            PurchaseTenders = new Collection<PurchaseTender>();
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PurchaseRequestId { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime RequestedDeliveryDate { get; set; }
        public int? SupplierCurrentAccountId { get; set; }
        public PurchaseTypeEnum PurchaseType { get; set; }
        public DateTime? FinalBidDateTime { get; set; }
        public int RequestedBlockId { get; set; }
        public string RequestedPlace { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public double InvoiceAmount { get; set; }
        public PurchaseStatusEnum PurchaseStatus { get; set; }
        public bool Delivered { get; set; }

        public virtual Project Project { get; set; }
        public virtual Product Product { get; set; }
        public virtual PurchaseRequest PurchaseRequest { get; set; }
        public virtual CurrentAccount CurrentAccount { get; set; }
        public virtual Block Block { get; set; }
        public virtual ICollection<PurchaseTender> PurchaseTenders { get; set; }
    }
}
