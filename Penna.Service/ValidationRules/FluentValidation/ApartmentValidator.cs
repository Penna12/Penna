using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ApartmentValidator : AbstractValidator<Apartment>
    {
        public ApartmentValidator()
        {
            RuleFor(p => p.Floor).NotNull().WithMessage("Lütfen pafta numarasını giriniz.");
            RuleFor(p => p.SectionName).NotEmpty().WithMessage("Lütfen, bölüm adını giriniz.");
            RuleFor(p => p.Gross).NotNull().WithMessage("Lütfen brüt m2 giriniz.");
            RuleFor(p => p.Net).NotNull().WithMessage("Lütfen net m2 giriniz.");
            RuleFor(p => p.Gabari).NotEmpty().WithMessage("Lütfen, gabari m2 giriniz.");

            RuleFor(p => p.BlockId).NotNull().WithMessage("Hata: Blok ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
