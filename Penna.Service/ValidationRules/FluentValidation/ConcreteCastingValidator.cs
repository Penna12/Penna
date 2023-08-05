using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ConcreteCastingValidator : AbstractValidator<ConcreteCasting>
    {
        public ConcreteCastingValidator()
        {
            RuleFor(p => p.CarPlate).NotEmpty().WithMessage("Lütfen, araç plakasını giriniz.");
            RuleFor(p => p.CastingDate).NotNull().WithMessage("Lütfen döküm tarihini giriniz.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen döküm miktarını giriniz.");
            RuleFor(p => p.Quantity).GreaterThanOrEqualTo(1).WithMessage("Lütfen, sıfırdan büyük miktar giriniz.");
            RuleFor(p => p.WaybilNumber).NotEmpty().WithMessage("Lütfen, irsaliye numarasını giriniz.");
        }
    }
}
