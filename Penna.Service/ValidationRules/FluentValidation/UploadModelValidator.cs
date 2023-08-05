using FluentValidation;
using Penna.Entities.DTOs;


namespace Penna.Business.ValidationRules.FluentValidation
{
    public class UploadModelValidator : AbstractValidator<UploadModelDto>
    {
        public UploadModelValidator()
        {
            RuleFor(d => d.Mimari).NotEmpty().WithMessage("Mimari dosya yükleyiniz.");
            RuleFor(d => d.Statik).NotEmpty().WithMessage("Statik dosya yükleyiniz.");
            RuleFor(d => d.Elektrik).NotEmpty().WithMessage("Elektrik dosya yükleyiniz.");
            RuleFor(d => d.Mekanik).NotEmpty().WithMessage("Mekanik dosya yükleyiniz.");
            RuleFor(d => d.Message).NotEmpty().WithMessage("Lütfen yapılan değişiklikler mesajınızı giriniz.");
        }
    }
}
