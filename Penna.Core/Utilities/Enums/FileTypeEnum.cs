using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum FileTypeEnum : byte
    {
        [Display(Name = "Mimari")]
        Mimari = 1,
        [Display(Name = "Statik")]
        Statik,
        [Display(Name = "Mekanik")]
        Mekanik,
        [Display(Name = "Elektrik")]
        Elektrik,
        [Display(Name = "Peyzaj")]
        Peyzaj
    }
}
