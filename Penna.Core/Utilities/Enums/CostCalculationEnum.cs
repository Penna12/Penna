using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum CostCalculationEnum : byte
    {
        [Display(Name = "Net m2")]
        Net_m2 = 1,
        [Display(Name = "Brüt m2")]
        Brut_m2,
        [Display(Name = "Gabari m2")]
        Gabari_m2,
    }
}
