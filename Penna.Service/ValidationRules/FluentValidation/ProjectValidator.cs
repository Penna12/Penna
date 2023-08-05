using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(p => p.ProjectName).NotEmpty().WithMessage("Lütfen, proje adını giriniz.");
            RuleFor(p => p.CurrentAccountId).NotNull().WithMessage("Lütfen, işveren firma seçiniz.");
            RuleFor(p => p.ArchitecturalWorks).NotEmpty().WithMessage("Lütfen, mimari firma adını giriniz.");
            RuleFor(p => p.StaticWorks).NotEmpty().WithMessage("Lütfen, statik işleri firma adını giriniz.");
            RuleFor(p => p.MechanicalWorks).NotEmpty().WithMessage("Lütfen, mekanik işler firma adını giriniz.");
            RuleFor(p => p.ElectricalWorks).NotEmpty().WithMessage("Lütfen, elektrik işleri firma adını giriniz.");

            RuleFor(p => p.Pafta).NotNull().WithMessage("Lütfen pafta numarasını giriniz.");
            RuleFor(p => p.Pafta).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");
            RuleFor(p => p.Ada).NotNull().WithMessage("Lütfen ada numarasını giriniz.");
            RuleFor(p => p.Ada).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");
            RuleFor(p => p.Parsel).NotNull().WithMessage("Lütfen parsel numarasını giriniz.");
            RuleFor(p => p.Parsel).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");

            RuleFor(p => p.LicenseNumber).NotEmpty().WithMessage("Lütfen, ruhsat numarasını giriniz.");
            RuleFor(p => p.LicenseDate).NotEmpty().WithMessage("Lütfen, ruhsat tarihini giriniz.");

            RuleFor(p => p.CountryId).NotNull().WithMessage("Lütfen ülke seçiniz.");
            RuleFor(p => p.CityId).NotNull().WithMessage("Lütfen şehir seçiniz.");
            RuleFor(p => p.TownId).NotNull().WithMessage("Lütfen kasaba seçiniz.");

            RuleFor(p => p.BuildingInspection).NotEmpty().WithMessage("Lütfen, yapı denetimi giriniz.");
            RuleFor(p => p.EmploymentDate).NotNull().WithMessage("Lütfen iş alım tarihini giriniz.");
            RuleFor(p => p.CommitmentDate).NotNull().WithMessage("Lütfen taahhüt tarihini giriniz.");

            RuleFor(p => p.BlockCount).NotNull().WithMessage("Lütfen blok sayısı giriniz.");
            RuleFor(p => p.BlockCount).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");
            RuleFor(p => p.TotalApartmentCount).NotNull().WithMessage("Lütfen toplam bağımsız bölüm sayısı giriniz.");
            RuleFor(p => p.TotalApartmentCount).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");
            RuleFor(p => p.TotalCommercialCount).NotNull().WithMessage("Lütfen ticari bölüm sayısı giriniz.");
            RuleFor(p => p.TotalCommercialCount).InclusiveBetween(1, int.MaxValue).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");

            RuleFor(p => p.CommitmentAmount).NotNull().WithMessage("Lütfen taahhüt tutarını giriniz.");
            RuleFor(p => p.CommitmentAmount).InclusiveBetween(1, double.MaxValue).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");
            RuleFor(p => p.DownPaymentRate).NotNull().WithMessage("Lütfen peşinat oranını giriniz.");
            RuleFor(p => p.DownPaymentRate).InclusiveBetween(1, float.MaxValue).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");
            RuleFor(p => p.InstallmentCount).NotNull().WithMessage("Lütfen taksit sayısını giriniz.");
            RuleFor(p => p.InstallmentCount).InclusiveBetween((byte)0, (byte)254).WithMessage("Lütfen sıfırdan büyük bir sayı giriniz.");

            RuleFor(p => p.TenantId).NotNull().WithMessage("Hata: Firma ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
