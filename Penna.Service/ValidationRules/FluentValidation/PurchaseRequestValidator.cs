using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class PurchaseRequestValidator : AbstractValidator<PurchaseRequest>
    {
        public PurchaseRequestValidator()
        {
            RuleFor(p => p.ProductId).NotNull().WithMessage("Lütfen, ürün seçiniz.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen, miktarı giriniz.");
            RuleFor(p => p.Quantity).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen, miktarı sıfırdan büyük giriniz.");
            RuleFor(p => p.Deadline).NotNull().WithMessage("Lütfen, termin tarihini giriniz.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Lütfen bir açıklama giriniz.");
        }
    }
}
