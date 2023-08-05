using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum PurchaseStatusEnum : byte
    {
        [Display(Name = "Satın Alma Bekliyor")]
        Pending = 1,
        [Display(Name = "Satın Alındı")]
        Bought
    }
}
