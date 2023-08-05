using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ConcreteRequestValidator : AbstractValidator<ConcreteRequest>  
    {
        public ConcreteRequestValidator()
        {
            RuleFor(p => p.TransactionDate).NotNull().WithMessage("Lütfen, tarihi giriniz.");
            RuleFor(p => p.ProductId).NotNull().WithMessage("Lütfen, beton türünü seçiniz.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen, istenen miktarı giriniz.");
            RuleFor(p => p.Quantity).GreaterThanOrEqualTo(1).WithMessage("Lütfen, sıfırdan büyük miktar giriniz.");
            //RuleFor(p => p.Quantity).Must(GreaterThanZero).WithMessage("sıfırdan büyük miktar giriniz.");
            //RuleFor(p => p.Quantity).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen, sıfırdan büyük miktar giriniz.");
            RuleFor(p => p.RequestedDate).NotNull().WithMessage("Lütfen, istenilen tarihi giriniz.");
            RuleFor(p => p.BlockId).NotNull().WithMessage("Lütfen, talep yerini seçiniz.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Lütfen, açıklama giriniz.");

            //RuleFor(p => p.Quantity).GreaterThanOrEqualTo(1);
            //RuleFor(p => p.Quantity).GreaterThanOrEqualTo(10).When(p => p.Quantity == 0);
            //RuleFor(p => p.Title).Must(StartWithWithA);
        }

        //private bool StartWithWithA(string arg)
        //{
        //    return arg.StartsWith("A");
        //}

        private bool GreaterThanZero(int arg)
        {
            return  arg > 0;
        }

        private bool NotNullAndGreaterThanNow(System.DateTime arg)
        {
            return arg != null && arg >= System.DateTime.Now;
        }
    }
}
