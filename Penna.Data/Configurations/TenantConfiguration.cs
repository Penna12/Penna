using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.CityId).IsRequired();
            builder.Property(x => x.CountryDialCode).HasMaxLength(5);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Email).HasMaxLength(60);
            builder.Property(x => x.TaxId).IsRequired().HasMaxLength(11);
            builder.Property(x => x.TaxOffice).HasMaxLength(25);
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Country)
                .WithMany(d => d.Tenants)
                .HasForeignKey(m => m.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tenants_Countries_CountryId");

            builder.HasOne(m => m.City)
                .WithMany(d => d.Tenants)
                .HasForeignKey(m => m.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tenants_Cities_CityId");
        }
    }
}
