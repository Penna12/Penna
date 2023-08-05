using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class CurrentAccountConfiguration : IEntityTypeConfiguration<CurrentAccount>
    {
        public void Configure(EntityTypeBuilder<CurrentAccount> builder)
        {
            builder.ToTable("CurrentAccounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.AuthorizedPerson).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
            builder.Property(x => x.TaxId).IsRequired().HasMaxLength(11);
            builder.Property(x => x.TaxOffice).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Phone1).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Phone2).HasMaxLength(30);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BankName).HasMaxLength(50);
            builder.Property(x => x.IBAN).HasMaxLength(33); 
            builder.Property(x => x.CompanyStatusId).IsRequired();

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");
            
            //builder.HasAlternateKey(e => e.Email).HasName("UXC_CurrentAccounts_Email");
            builder.HasIndex(x => x.Email).IsUnique().HasName("UXC_CurrentAccounts_Email");

            builder.HasOne(m => m.Tenant)
                .WithMany(d => d.CurrentAccounts)
                .HasForeignKey(m => m.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentAccounts_Tenant_TenantId");

            builder.HasOne(m => m.Country)
                .WithMany(d => d.CurrentAccounts)
                .HasForeignKey(m => m.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentAccounts_Country_CountryId");

            builder.HasOne(m => m.City)
                .WithMany(d => d.CurrentAccounts)
                .HasForeignKey(m => m.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentAccounts_City_CityId");

            builder.HasOne(m => m.Town)
                .WithMany(d => d.CurrentAccounts)
                .HasForeignKey(m => m.TownId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentAccounts_Town_TownId");
        }
    }
}
