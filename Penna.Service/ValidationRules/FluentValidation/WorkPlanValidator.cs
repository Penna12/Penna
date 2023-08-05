using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class WorkPlanValidator : AbstractValidator<WorkPlan>
    {
        public WorkPlanValidator()
        {
            RuleFor(p => p.WorkName).NotEmpty().WithMessage("Lütfen, planlanan görev adını giriniz.");
            RuleFor(p => p.StartingDate).NotNull().WithMessage("Lütfen, başlama tarihini giriniz.");
            RuleFor(p => p.Duration).NotNull().WithMessage("Lütfen, iş tamamlanma süresini giriniz.");
            RuleFor(p => p.ContractorCurrentAccountId).NotNull().WithMessage("Lütfen, iş atanan taşeron seçimini yapınız.");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Lütfen, atanan iş miktarını giriniz.");
            RuleFor(p => p.UnitId).NotNull().WithMessage("Lütfen, birim seçiniz.");
        }
    }
}
