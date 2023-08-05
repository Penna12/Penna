using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class LaborAddDtoValidator : AbstractValidator<LaborAddDto>
    {
        public LaborAddDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, işçilik adı giriniz.");
            RuleFor(p => p.Info).NotEmpty().WithMessage("Lütfen, işçilik tanımı giriniz.");
            RuleFor(p => p.TenantId).NotNull().WithMessage("Hata: Firma ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
