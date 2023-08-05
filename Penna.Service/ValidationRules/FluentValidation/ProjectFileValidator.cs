using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ProjectFileValidator : AbstractValidator<ProjectFile>
    {
        public ProjectFileValidator()
        {
            RuleFor(p => p.FilePath).NotEmpty().WithMessage("Lütfen, dosya yolu boş olamaz.");
            RuleFor(p => p.FileTypeId).NotNull().WithMessage("Lütfen dosya tipi boş olamaz.");
            RuleFor(p => p.Message).NotEmpty().WithMessage("Lütfen, message boş olamaz.");
        }
    }
}
