using Penna.Core.Entities;
using System;

namespace Penna.Entities.Models
{
    public class PurchaseTender : BaseModel, IEntity
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; } // Satın Alma
        public int SupplierCurrentAccountId { get; set; } // Tedarikçi
        public DateTime? OfferTime { get; set; }// Teklif Zamanı
        public double? Amount { get; set; } // Teklif Tutarı
        public bool Joined { get; set; } // İhaleye Katıldı mı?
        public bool Won { get; set; } // İhaleyi Kazandı mı?

        public virtual Purchase Purchase { get; set; }
        public virtual CurrentAccount CurrentAccount { get; set; }
    }
}
