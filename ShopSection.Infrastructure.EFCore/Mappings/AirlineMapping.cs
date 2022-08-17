using ShopSection.Domain.AirlineAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopSection.Infrastructure.EFCore.Mappings
{
    public class AirlineMapping : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder.ToTable("Airlines");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(1000);
            builder.Property(x => x.Info).HasMaxLength(3000);
        }
    }
}
