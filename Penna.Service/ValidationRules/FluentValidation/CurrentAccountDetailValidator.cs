using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class CurrentAccountDetailValidator  :AbstractValidator<CurrentAccountDetail>
    {
        public CurrentAccountDetailValidator()
        {
            RuleFor(p => p.CurDate).NotEmpty().WithMessage("Lütfen, tarih giriniz.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Lütfen, açıklama giriniz.");
            RuleFor(p => p.Debit).NotNull().WithMessage("Lütfen, borç tutarı giriniz.");
            RuleFor(p => p.Credit).NotNull().WithMessage("Lütfen, alacak tutarı giriniz.");
            RuleFor(p => p.ProjectId).NotNull().WithMessage("Hata: Proje ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
            RuleFor(p => p.CurrentAccountId).NotNull().WithMessage("Hata: CariHesap ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
