using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.Product.Name).NotEmpty().WithMessage("Lütfen, ürün ismi giriniz.");
            //RuleFor(p => p.Product.Brand).NotEmpty().WithMessage("Lütfen, marka giriniz.");
            RuleFor(p => p.Product.UnitId).NotEmpty().WithMessage("Lütfen, birimini belirtiniz.");
            //RuleFor(p => p.Product.Quantity).NotNull().WithMessage("Lütfen, miktarını belirtiniz.");
            //RuleFor(p => p.Product.Species).NotEmpty().WithMessage("Lütfen, tür/cins belirtiniz.");
            RuleFor(p => p.Product.BusinessGroupId).NotNull().WithMessage("Lütfen, iş grubunu belirtiniz.");
            RuleFor(p => p.Product.Status).NotNull().WithMessage("Lütfen, durumunu belirtiniz.");
            RuleFor(p => p.Product.ProjectId).NotNull().WithMessage("Hata: Proje ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
