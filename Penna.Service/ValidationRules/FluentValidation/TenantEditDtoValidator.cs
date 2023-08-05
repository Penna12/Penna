using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class TenantEditDtoValidator : AbstractValidator<TenantEditDto>
    {
        public TenantEditDtoValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage("ID boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz. ");
            RuleFor(p => p.Title).NotEmpty().WithMessage("Ünvan girmelisiniz.");
            RuleFor(p => p.FullName).NotEmpty().WithMessage("Ad Soyad girmelisiniz.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Eposta girmelisiniz.");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Telefon girmelisiniz.");
            //RuleFor(p => p.Address).NotEmpty().WithMessage("Adres girmelisiniz.");
            RuleFor(p => p.CityId).NotNull().WithMessage("Şehir seçmelisiniz.");
            RuleFor(p => p.CountryId).NotNull().WithMessage("Ülke seçmelisiniz.");
            RuleFor(p => p.TaxId).NotEmpty().WithMessage("Vergi/TC Kimlik no girmelisiniz.");
            RuleFor(p => p.TaxId).MinimumLength(10).WithMessage("Vergi/TC Kimlik no min 10 karakter girebilirsiniz.");
            RuleFor(p => p.TaxId).MaximumLength(11).WithMessage("Vergi/TC Kimlik no max 11 karakter girebilirsiniz.");
            //RuleFor(p => p.TaxOffice).NotEmpty().WithMessage("Vergi dairesi girmelisiniz.");
        }
    }
}
