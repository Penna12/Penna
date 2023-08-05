using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class ConcreteRequestConfiguration : IEntityTypeConfiguration<ConcreteRequest>
    {
        public void Configure(EntityTypeBuilder<ConcreteRequest> builder)
        {
            builder.ToTable("ConcreteRequest");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TransactionDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.RequestedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.Description).IsRequired().HasMaxLength(150);

            builder.HasOne(m => m.Block)
                .WithMany(d => d.ConcreteRequests)
                .HasForeignKey(d => d.BlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConcreteRequests_Block_BlockId");

            builder.HasOne(m => m.Product)
                .WithMany(d => d.ConcreteRequests)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConcreteRequests_Product_ProductId");
        }
    }
}
