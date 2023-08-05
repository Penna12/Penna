using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class UnitValidator : AbstractValidator<Unit>
    {
        public UnitValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, birim adı giriniz.");
        }
    }
}
