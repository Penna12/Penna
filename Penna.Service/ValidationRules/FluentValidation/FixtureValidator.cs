using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class FixtureValidator : AbstractValidator<Fixture>
    {
        public FixtureValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Lütfen, ürün adını giriniz.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen, miktarı giriniz.");
        }
    }
}
