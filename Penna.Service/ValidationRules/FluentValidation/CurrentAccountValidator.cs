using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class CurrentAccountValidator  :AbstractValidator<CurrentAccount>
    {
        public CurrentAccountValidator()
        {
            RuleFor(p => p.CompanyName).NotEmpty().WithMessage("Lütfen, firma ismi giriniz.");
            RuleFor(p => p.AuthorizedPerson).NotEmpty().WithMessage("Lütfen, yetkili kişiyi giriniz.");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Lütfen, adres giriniz.");
            RuleFor(p => p.CountryId).NotNull().WithMessage("Lütfen, ülke seçimi yapınız.");
            RuleFor(p => p.CityId).NotNull().WithMessage("Lütfen, şehir seçimi yapınız.");
            RuleFor(p => p.TownId).NotNull().WithMessage("Lütfen, ilçe seçimi yapınız.");
            RuleFor(p => p.TaxId).NotNull().WithMessage("Lütfen, vergi no giriniz.");
            RuleFor(p => p.TaxOffice).NotEmpty().WithMessage("Lütfen, vergi dairesini giriniz.");
            RuleFor(p => p.Phone1).NotEmpty().WithMessage("Lütfen, en az bir telefon no giriniz.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Lütfen, email giriniz.");
            RuleFor(p => p.AccountTypeId).NotNull().WithMessage("Lütfen, hesap tipini belirtiniz.");
            RuleFor(p => p.CompanyStatusId).NotNull().WithMessage("Lütfen, firma durumu belirtiniz.");
            RuleFor(p => p.TenantId).NotNull().WithMessage("Hata: Firma ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
