using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class UnitDtoValidator : AbstractValidator<UnitDto>
    {
        public UnitDtoValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage("ID boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz. ");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, birim adı giriniz.");
        }
    }
}
