using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class UserPositionAuthorityValidator : AbstractValidator<UserPositionAuthority>
    {
        public UserPositionAuthorityValidator()
        {
            RuleFor(p => p.AuthorityId).NotNull().WithMessage("Lütfen, yetki boş olamaz.");
            RuleFor(p => p.UserPositionId).NotNull().WithMessage("Lütfen, görev boş olamaz.");
        }
    }
}
