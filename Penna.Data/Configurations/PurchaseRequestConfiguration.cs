using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class PurchaseRequestConfiguration : IEntityTypeConfiguration<PurchaseRequest>
    {
        public void Configure(EntityTypeBuilder<PurchaseRequest> builder)
        {
            builder.ToTable("PurchaseRequest");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.Deadline).IsRequired().HasColumnType("datetime");

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Project)
                .WithMany(d => d.PurchaseRequests)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseRequests_Project_ProjectId");

            builder.HasOne(m => m.Product)
                .WithMany(d => d.PurchaseRequests)
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseRequests_Product_ProductId");
        }
    }
}
