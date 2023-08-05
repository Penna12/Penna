using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum ProjectStatusEnum:byte
    {
        [Display(Name = "Planlanıyor")]
        Planning= 1,
        [Display(Name = "Aktif")]
        Active,
        [Display(Name = "Kapalı")]
        Closed,
        [Display(Name = "Arşivlendi")]
        Archived,
        [Display(Name = "Silindi")]
        Deleted
    }
}
