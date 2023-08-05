using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class OfferCloseDtoValidator : AbstractValidator<OfferCloseDto>
    {
        public OfferCloseDtoValidator()
        {
            RuleFor(p => p.InvoiceDate).NotNull().WithMessage("Lütfen fatura tarihini giriniz");
            RuleFor(p => p.Deadline).NotNull().WithMessage("Lütfen termin tarihini giriniz");
            RuleFor(p => p.InvoiceNo).NotEmpty().WithMessage("Lütfen fatura no giriniz");
            RuleFor(p => p.InvoiceAmount).NotNull().WithMessage("Lütfen, fatura tutarını giriniz.");
            RuleFor(p => p.InvoiceAmount).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen, fatura tutarını sıfırdan büyük giriniz.");
        }
    }
}
