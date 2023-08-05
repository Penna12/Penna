using Penna.Core.Entities;
using System;

namespace Penna.Entities.Models
{
    public class CurrentAccountDetail : BaseModel, IEntity
    {
        public int Id { get; set; }
        public int CurrentAccountId { get; set; } // Cari ID
        public DateTime CurDate { get; set; } // Tarih
        public string Description { get; set; } // Açıklama
        public double Debit { get; set; } // Borç
        public double Credit { get; set; } // Alacak
        public int? ProjectId { get; set; } // Proje ID
        public byte? ProjectInstallmentNo { get; set; }

        public virtual Project Project { get; set; }
        public virtual CurrentAccount CurrentAccount { get; set; }
    }
}
