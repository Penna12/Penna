using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, ürün ismi giriniz.");
            //RuleFor(p => p.Brand).NotEmpty().WithMessage("Lütfen, marka giriniz.");
            RuleFor(p => p.Unit).NotEmpty().WithMessage("Lütfen, birimini belirtiniz.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen, miktarını belirtiniz.");
            RuleFor(p => p.Quantity).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen, miktarı sıfırdan büyük giriniz.");
            //RuleFor(p => p.Species).NotEmpty().WithMessage("Lütfen, tür/cins belirtiniz.");
            RuleFor(p => p.BusinessGroupId).NotNull().WithMessage("Lütfen, iş grubunu belirtiniz.");
            RuleFor(p => p.Status).NotNull().WithMessage("Lütfen, durumunu belirtiniz.");
            RuleFor(p => p.ProjectId).NotNull().WithMessage("Hata: Proje ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
            //RuleFor(p => p.Price).NotNull().WithMessage("Lütfen, ürün fiyatı giriniz.");
            //RuleFor(p => p.Price).InclusiveBetween(1, decimal.MaxValue).WithMessage("Lütfen, ürün fiyatı sıfırdan büyük giriniz.");
            //RuleFor(p => p.Stock).NotNull().WithMessage("Lütfen, stok miktarını giriniz.");
            //RuleFor(p => p.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen, stok miktarını sıfırdan büyük giriniz.");
        }
    }
}
