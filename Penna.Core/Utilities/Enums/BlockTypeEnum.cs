using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum BlockTypeEnum : byte
    {
        [Display(Name = "Blok")]
        Block = 1,
        [Display(Name = "Ortak Alan")]
        PublicArea,
    }
}
