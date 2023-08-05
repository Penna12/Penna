using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class BlockTeamValidator : AbstractValidator<BlockTeam>
    {
        public BlockTeamValidator()
        {
            RuleFor(p => p.UserId).NotNull().WithMessage("Lütfen bir kullanıcı seçiniz.");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Lütfen, telefon no giriniz.");
            RuleFor(p => p.UserPositionId).NotNull().WithMessage("Lütfen görev seçiniz.");

            RuleFor(p => p.BlockId).NotNull().WithMessage("Hata: Blok ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
