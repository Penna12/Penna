using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class BlockSubcontractorAddDtoValidator : AbstractValidator<BlockSubcontractorAddDto>
    {
        public BlockSubcontractorAddDtoValidator()
        {
            RuleFor(p => p.CurrentAccountId).NotNull().WithMessage("Lütfen taşeron firma seçiniz.");
            RuleFor(p => p.CompanyRepresentative).NotEmpty().WithMessage("Lütfen, firma temsilcisini giriniz.");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Lütfen, erişim numarası giriniz.");
            RuleFor(p => p.BusinessGroupId).NotNull().WithMessage("Lütfen yüklendiği işi seçiniz.");
        }
    }
}
