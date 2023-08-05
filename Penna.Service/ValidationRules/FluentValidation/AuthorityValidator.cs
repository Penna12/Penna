using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class AuthorityValidator : AbstractValidator<Authority>
    {
        public AuthorityValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, yetki adını giriniz.");
        }
    }
}
