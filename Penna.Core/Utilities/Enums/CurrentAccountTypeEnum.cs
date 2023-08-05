using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum CurrentAccountTypeEnum : byte
    {
        [Display(Name = "Proje Sahibi")]
        ProjectOwner = 1,
        [Display(Name = "Tedarikçi")]
        Vendor,
        [Display(Name = "Taşeron")]
        Subcontractor,
        [Display(Name = "Müşteri/Malik")]
        Customer,
    }
}
