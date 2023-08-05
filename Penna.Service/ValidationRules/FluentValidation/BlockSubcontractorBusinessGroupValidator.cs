using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class BlockSubcontractorBusinessGroupValidator : AbstractValidator<BlockSubcontractorBusinessGroup>
    {
        public BlockSubcontractorBusinessGroupValidator()
        {
            RuleFor(p => p.BlockSubcontractorId).NotNull().WithMessage("Lütfen taşeron firma seçiniz.");
            RuleFor(p => p.BusinessGroupId).NotNull().WithMessage("Lütfen yüklendiği işi seçiniz.");
        }
    }
}
