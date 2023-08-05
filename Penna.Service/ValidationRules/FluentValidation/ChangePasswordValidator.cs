using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(p => p.CurrentPassword).NotEmpty().WithMessage("Kullanmakta olduğunuz mevcut parolanızı giriniz.");
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Yeni parola giriniz.");
            RuleFor(p => p.ConfirmNewPassword).NotEmpty().WithMessage("Yeni paralanızı tekrar giriniz");
            RuleFor(p => p.ConfirmNewPassword).Equal(p => p.NewPassword).WithMessage("Parolalar uyuşmuyor");
        }
    }
}
