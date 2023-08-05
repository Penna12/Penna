using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class FixtureEmbezzledValidator : AbstractValidator<FixtureEmbezzled>
    {
        public FixtureEmbezzledValidator()
        {
            RuleFor(p => p.AppUserId).NotEmpty().WithMessage("Lütfen, zimmet yapılacak kişiyi seçiniz.");
            RuleFor(p => p.EmbezzledDate).NotNull().WithMessage("Lütfen, zimmet yapıldığı tarihi giriniz.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen, verilen miktarı giriniz.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Lütfen, açıklama giriniz.");
        }
    }
}
