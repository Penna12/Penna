using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ProductInOutValidator : AbstractValidator<ProductInOut>
    {
        public ProductInOutValidator()
        {
            //RuleFor(p => p.DispatchPoint).NotEmpty().WithMessage("Lütfen, sevk noktasını giriniz.");
            RuleFor(p => p.ProductId).NotNull().WithMessage("Lütfen, ürün seçiniz.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen, miktarı giriniz.");
            RuleFor(p => p.Quantity).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen, miktarı sıfırdan büyük giriniz.");
            RuleFor(p => p.TransactionDate).NotNull().WithMessage("Lütfen, işlem tarihini giriniz.");
        }
    }
}
