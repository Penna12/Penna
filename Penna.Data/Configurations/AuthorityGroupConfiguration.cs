using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class AthoritGroupConfiguration : IEntityTypeConfiguration<AuthorityGroup>
    {
        public void Configure(EntityTypeBuilder<AuthorityGroup> builder)
        {
            builder.ToTable("AuthorityGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);

            builder.HasOne(m => m.Tenant)
                .WithMany(d => d.AuthorityGroups)
                .HasForeignKey(m => m.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuthorityGroup_Tenants_TenantId");
        }
    }
}
