using AccountSection.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountSection.Infrastructure.EFCore.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.PhoneNumber).HasMaxLength(15);
            builder.Property(x => x.VerificationCode).HasMaxLength(10);
            builder.Property(x => x.Email).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Fullname).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
            builder.HasOne(x => x.Role).WithMany(x => x.Accounts).HasForeignKey(x => x.RoleId);
        }
    }
}
