using FluentValidation;
using Penna.Entities.DTOs;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class AppUserEditDtoValidator : AbstractValidator<AppUserEditDto>
    {
        public AppUserEditDtoValidator()
        {
            RuleFor(p => p.FullName).NotEmpty().WithMessage("Ad Soyad gereklidir");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email gereklidir");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Telefon no gereklidir");
            RuleFor(p => p.CityId).NotNull().WithMessage("Şehir gereklidir");
            RuleFor(p => p.CountryId).NotNull().WithMessage("Ülke gereklidir");
        }
    }
}
