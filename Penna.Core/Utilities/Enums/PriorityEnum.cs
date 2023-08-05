using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum PriorityEnum : byte
    {
        [Display(Name = "Düşük")]
        Low = 1,
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "Yüksek")]
        High,
        [Display(Name = "Acil")]
        Urgent
    }
}
