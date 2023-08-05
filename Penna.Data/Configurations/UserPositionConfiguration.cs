using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class UserPositionConfiguration : IEntityTypeConfiguration<UserPosition>
    {
        public void Configure(EntityTypeBuilder<UserPosition> builder)
        {
            builder.ToTable("UserPositions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);

            builder.HasOne(m => m.AuthorityGroup)
                .WithMany(d => d.UserPositions)
                .HasForeignKey(m => m.AuthorityGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPositions_AuthorityGroup_AuthorityGroupId");

            builder.HasOne(m => m.Tenant)
                .WithMany(d => d.UserPositions)
                .HasForeignKey(m => m.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPosition_Tenants_TenantId");

        }
    }
}
