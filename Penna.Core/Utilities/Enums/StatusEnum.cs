using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum StatusEnum:byte
    {
        [Display(Name = "Pasif")]
        Pasif = 0,
        [Display(Name = "Aktif")]
        Aktif
    }
}
