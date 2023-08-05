using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class LaborValidator : AbstractValidator<Labor>
    {
        public LaborValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, işçilik adı giriniz.");
            RuleFor(p => p.Info).NotEmpty().WithMessage("Lütfen, işçilik tanımı giriniz.");
            RuleFor(p => p.TenantId).NotNull().WithMessage("Hata: Firma ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
