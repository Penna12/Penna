using FluentValidation;
using Penna.Entities.Models;

namespace Penna.Business.ValidationRules.FluentValidation
{
    public class BlockValidator : AbstractValidator<Block>
    {
        public BlockValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen, blok adı giriniz.");
            RuleFor(p => p.TypeId).NotNull().WithMessage("Lütfen blok türü seçiniz.");
            RuleFor(p => p.FloorCount).NotNull().WithMessage("Lütfen kat sayısı giriniz.");
            RuleFor(p => p.BasementCount).NotNull().WithMessage("Lütfen bodrum kat sayısı giriniz.");
            RuleFor(p => p.ApartmentCount).NotNull().WithMessage("Lütfen kat başına bağımsız bölüm sayısı giriniz.");
            RuleFor(p => p.BlockCostCalculation).NotNull().WithMessage("Lütfen blok maliyet hesaplama türünü belirtiniz.");
            RuleFor(p => p.ApartmentCostCalculation).NotNull().WithMessage("Lütfen daire maliyet hesaplama türünü belirtiniz.");
            RuleFor(p => p.PublicAreaCostCalculation).NotNull().WithMessage("Lütfen ortak alan hesaplama türünü belirtiniz.");
            RuleFor(p => p.ProjectId).NotNull().WithMessage("Hata: Proje ID'si boş geliyor!!!. Lütfen destek ekibini bilgilendiriniz.");
        }
    }
}
