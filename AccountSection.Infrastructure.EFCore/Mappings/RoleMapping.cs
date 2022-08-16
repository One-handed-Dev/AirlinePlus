using Microsoft.EntityFrameworkCore;
using AccountSection.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountSection.Infrastructure.EFCore.Mappings
{
    class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable($"{nameof(Role)}s");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.HasMany(x => x.Accounts).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);

            builder.OwnsMany(x => x.Permissions, navigationBuilder =>
            {
                navigationBuilder.ToTable("RolePermissions");
                navigationBuilder.HasKey(x => x.Id);
                navigationBuilder.Ignore(x => x.Name);
                navigationBuilder.WithOwner(x => x.Role);
            });
        }
    }
}
