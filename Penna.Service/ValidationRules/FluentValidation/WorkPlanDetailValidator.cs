using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class WorkPlanDetailValidator : AbstractValidator<WorkPlanDetail>
    {
        public WorkPlanDetailValidator()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage("Lütfen, açıklama giriniz.");
            RuleFor(p => p.AssignedWorkAmount).NotNull().WithMessage("Lütfen, atanmış iş miktarı boş olamaz.");
            RuleFor(p => p.UnitId).NotNull().WithMessage("Lütfen, birim seçilmeli.");
            //RuleFor(p => p.WorkPlanId).NotNull().WithMessage("Lütfen, ");
        }
    }
}
