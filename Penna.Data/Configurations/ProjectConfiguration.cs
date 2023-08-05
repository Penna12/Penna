using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProjectName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LicenseNumber).IsRequired().HasMaxLength(25);
            builder.Property(x => x.LicenseDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.EmploymentDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.CommitmentDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.BuildingInspection).IsRequired().HasMaxLength(50);

            builder.Property(x => x.ArchitecturalWorks).IsRequired().HasMaxLength(100);
            builder.Property(x => x.StaticWorks).IsRequired().HasMaxLength(100);
            builder.Property(x => x.MechanicalWorks).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ElectricalWorks).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LandScapeWorks).HasMaxLength(100);

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Tenant)
                .WithMany(d => d.Projects)
                .HasForeignKey(m => m.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Tenants_TenantId");

            builder.HasOne(m => m.CurrentAccount)
                .WithMany(d => d.Projects)
                .HasForeignKey(m => m.CurrentAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_CurrentAccount_CurrentAccountId");

            builder.HasOne(m => m.Country)
                .WithMany(d => d.Projects)
                .HasForeignKey(m => m.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Countries_CountryId");

            builder.HasOne(m => m.City)
                .WithMany(d => d.Projects)
                .HasForeignKey(m => m.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Cities_CityId");

            builder.HasOne(m => m.Town)
                .WithMany(d => d.Projects)
                .HasForeignKey(m => m.TownId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Towns_TownId");
        }
    }
}
