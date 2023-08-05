using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class UserPositionValidator : AbstractValidator<UserPosition>
    {
        public UserPositionValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, personelin görevini giriniz.");
        }
    }
}
