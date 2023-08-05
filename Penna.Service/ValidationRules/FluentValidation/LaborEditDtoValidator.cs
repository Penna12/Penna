using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class LaborEditDtoValidator : AbstractValidator<LaborEditDto>
    {
        public LaborEditDtoValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage("ID boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz. ");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, işçilik adı giriniz.");
            RuleFor(p => p.Info).NotEmpty().WithMessage("Lütfen, işçilik tanımı giriniz.");
            RuleFor(p => p.TenantId).NotNull().WithMessage("Hata: Firma ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
