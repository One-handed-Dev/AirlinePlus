using Microsoft.EntityFrameworkCore;
using AccountSection.Domain.ForgetPasswordLogAgg;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountSection.Infrastructure.EFCore.Mappings
{
    public class ForgetPasswordLogMapping : IEntityTypeConfiguration<ForgetPasswordLog>
    {
        public void Configure(EntityTypeBuilder<ForgetPasswordLog> builder)
        {
            builder.ToTable("ForgetPasswordLogs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Hash).HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).HasMaxLength(15);
        }
    }
}
