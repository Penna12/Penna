using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class AuthorityGroupValidator : AbstractValidator<AuthorityGroup>
    {
        public AuthorityGroupValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, yetki grubu adını giriniz.");
        }
    }
}
