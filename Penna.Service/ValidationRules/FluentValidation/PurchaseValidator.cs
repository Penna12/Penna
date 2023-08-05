using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class PurchaseValidator : AbstractValidator<Purchase>
    {
        public PurchaseValidator()
        {
            RuleFor(p => p.PurchaseDate).NotNull().WithMessage("Lütfen satın alma tarihini giriniz");
            RuleFor(p => p.RequestedDeliveryDate).NotNull().WithMessage("Lütfen talep edilen teslim tarihini giriniz");
            RuleFor(p => p.ProductId).NotNull().WithMessage("Lütfen, ürün seçiniz.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen, miktarı giriniz.");
            RuleFor(p => p.Quantity).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen, miktarı sıfırdan büyük giriniz.");
        }
    }
}
