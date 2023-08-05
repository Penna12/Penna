using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class CurrentAccountDetailConfiguration : IEntityTypeConfiguration<CurrentAccountDetail>
    {
        public void Configure(EntityTypeBuilder<CurrentAccountDetail> builder)
        {
            builder.ToTable("CurrentAccountDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.CurDate).HasColumnType("datetime");
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Project)
                .WithMany(d => d.CurrentAccountDetails)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentAccountDetails_Project_ProjectId");


            builder.HasOne(m => m.CurrentAccount)
                .WithMany(d => d.CurrentAccountDetails)
                .HasForeignKey(m => m.CurrentAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentAccountDetails_CurrentAccount_CurrentAccountId");
        }
    }
}
