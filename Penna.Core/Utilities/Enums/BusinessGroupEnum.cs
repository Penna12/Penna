using System.ComponentModel.DataAnnotations;

namespace Penna.Core.Utilities.Enums
{
    public enum BusinessGroupEnum : byte
    {
        [Display(Name = "Kaba İşler")]
        KabaIsler = 1,
        [Display(Name = "İnce İşler")]
        InceIsler,
        [Display(Name = "Elektrik İşleri")]
        ElektrikIsleri,
        [Display(Name = "Mekanik İşler")]
        MekanikIsler,
        [Display(Name = "Peyzaj İşler")]
        PeyzajIsleri,
        [Display(Name = "Mobilya İşleri")]
        MobilyaIsleri,
        [Display(Name = "Dış Cephe")]
        DisCepheIsleri,
        [Display(Name = "Doğrama")]
        DogramaIsleri,
        [Display(Name = "Çatı")]
        CatiIsleri,
        [Display(Name = "Asansör")]
        AsansorIsleri
    }
}
/*Kaba İşler
İnce İşler
Elektrik İşleri
Mekanik İşleri
Peyzaj İşleri
Mobilya İşleri
Dış Cephe
Doğrama
Çatı
Asansör
*/