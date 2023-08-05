using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class SignUpUserModelValidator : AbstractValidator<SignUpUserModel>
    {
        public SignUpUserModelValidator()
        {
            RuleFor(p => p.FullName).NotEmpty().WithMessage("Adınızı ve Soyadınızı giriniz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Epostanızı giriniz");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Telefon numaranızı giriniz");
            RuleFor(p => p.Password).NotNull().WithMessage("Şifre giriniz.");
            RuleFor(p => p.ConfirmPassword).NotNull().WithMessage("Şifrenizi tekrar giriniz");
            RuleFor(p => p.ConfirmPassword).Equal(p => p.Password).WithMessage("iki şifre uyuşmuyor");
            RuleFor(p => p.TenantId).NotNull().WithMessage("Kiracı Firma ID'si boş geliyor.");
        }
    }
}
