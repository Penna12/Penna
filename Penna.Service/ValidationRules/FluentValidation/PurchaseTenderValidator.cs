using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class PurchaseTenderValidator : AbstractValidator<PurchaseTender>
    {
        public PurchaseTenderValidator()
        {
            RuleFor(p => p.OfferTime).NotNull().WithMessage("Lütfen teklif zamanını giriniz");
            RuleFor(p => p.Amount).NotNull().WithMessage("Lütfen, teklif miktarını giriniz.");
            RuleFor(p => p.Amount).InclusiveBetween(1, double.MaxValue).WithMessage("Lütfen, teklif miktarını sıfırdan büyük giriniz.");
        }
    }
}
