using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class SignInValidator : AbstractValidator<SignInModel>
    {
        public SignInValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Lütfen geçerli bir eposta giriniz.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Lütfen şifrenizi giriniz.");
        }
    }
}
