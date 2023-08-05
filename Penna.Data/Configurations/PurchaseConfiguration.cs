using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.RequestedPlace).HasMaxLength(100);
            builder.Property(x => x.InvoiceNo).HasMaxLength(10);
            builder.Property(x => x.PurchaseDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.RequestedDeliveryDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.FinalBidDateTime).HasColumnType("datetime");
            builder.Property(x => x.Deadline).HasColumnType("datetime");
            builder.Property(x => x.InvoiceDate).HasColumnType("datetime");

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Project)
                .WithMany(d => d.Purchases)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Project_ProjectId");

            builder.HasOne(m => m.Product)
                .WithMany(d => d.Purchases)
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Product_ProductId");

            builder.HasOne(m => m.PurchaseRequest)
                .WithMany(d => d.Purchases)
                .HasForeignKey(m => m.PurchaseRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_PurchaseRequest_PurchaseRequestId");

            builder.HasOne(m => m.CurrentAccount)
                .WithMany(d => d.Purchases)
                .HasForeignKey(m => m.SupplierCurrentAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_CurrentAccount_SupplierCurrentAccountId");

            builder.HasOne(m => m.Block)
                .WithMany(d => d.Purchases)
                .HasForeignKey(m => m.RequestedBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Block_RequestedBlockId");
        }
    }
}
