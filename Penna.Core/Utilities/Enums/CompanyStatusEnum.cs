using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum CompanyStatusEnum : byte
    {
        [Display(Name = "Beyaz Liste")]
        WhiteList = 1,
        [Display(Name = "Kara Liste")]
        BlackList
    }
}
