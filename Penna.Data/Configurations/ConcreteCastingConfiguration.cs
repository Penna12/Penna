using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;


namespace Penna.Data.Configurations
{
    public class ConcreteCastingConfiguration : IEntityTypeConfiguration<ConcreteCasting>
    {
        public void Configure(EntityTypeBuilder<ConcreteCasting> builder)
        {
            builder.ToTable("ConcreteCasting");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CarPlate).IsRequired().HasMaxLength(15);
            builder.Property(x => x.CastingDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.WaybilNumber).IsRequired().HasMaxLength(15);

            builder.HasOne(m => m.ConcreteRequest)
                .WithMany(d => d.ConcreteCastings)
                .HasForeignKey(d => d.ConcreteRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConcreteCasting_ConcreteRequest_ConcreteRequestId");
        }
    }
}
