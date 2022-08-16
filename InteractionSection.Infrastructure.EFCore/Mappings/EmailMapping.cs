using Microsoft.EntityFrameworkCore;
using InteractionSection.Domain.EmailAgg;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractionSection.Infrastructure.EFCore.Mappings
{
    public class EmailMapping : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("Emails");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.RecieverName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.RecieverEmail).HasMaxLength(511).IsRequired();
        }
    }
}
