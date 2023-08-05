using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class PurchaseTenderConfiguration : IEntityTypeConfiguration<PurchaseTender>
    {
        public void Configure(EntityTypeBuilder<PurchaseTender> builder)
        {
            builder.ToTable("PurchaseTender");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.OfferTime).HasColumnType("datetime");

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Purchase)
                .WithMany(d => d.PurchaseTenders)
                .HasForeignKey(m => m.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseTenders_Purchase_PurchaseId");

            builder.HasOne(m => m.CurrentAccount)
                .WithMany(d => d.PurchaseTenders)
                .HasForeignKey(m => m.SupplierCurrentAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseTenders_CurrentAccount_SupplierCurrentAccountId");
        }
    }
}
