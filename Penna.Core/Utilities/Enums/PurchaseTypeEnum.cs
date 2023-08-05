using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum PurchaseTypeEnum : byte
    {
        [Display(Name = "Doğrudan Satın Alma")]
        Direct = 1,
        [Display(Name = "Teklif Usulü Satın Alma")]
        Offer,
        [Display(Name = "İhale Usulü Satın Alma")]
        Tender
    }
}
