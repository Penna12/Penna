using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class BlockSubcontractorValidator : AbstractValidator<BlockSubcontractor>
    {
        public BlockSubcontractorValidator()
        {
            RuleFor(p => p.CurrentAccountId).NotNull().WithMessage("Lütfen taşeron firma seçiniz.");
            RuleFor(p => p.CompanyRepresentative).NotEmpty().WithMessage("Lütfen, firma temsilcisini giriniz.");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Lütfen, erişim numarası giriniz.");
            //RuleFor(p => p.BusinessGroupId).NotNull().WithMessage("Lütfen yüklendiği işi seçiniz.");

            RuleFor(p => p.BlockId).NotNull().WithMessage("Hata: Blok ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
