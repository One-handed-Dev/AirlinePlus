using ShopSection.Domain.OrderAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopSection.Infrastructure.EFCore.Mappings
{
    public sealed class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders"); 

            builder.HasKey(x => x.Id);
            builder.Property(x => x.RefId).HasMaxLength(1000);
            builder.Property(x => x.IssueTrackingNo).HasMaxLength(63);

            builder.OwnsMany(x => x.Items, itemsBuilder =>
            {
                itemsBuilder.ToTable("OrderItems");

                itemsBuilder.HasKey(x => x.Id);
                itemsBuilder.Property(x => x.EndDatePr).HasMaxLength(50);
                itemsBuilder.Property(x => x.StartDatePr).HasMaxLength(50);
                itemsBuilder.WithOwner(x => x.Order).HasForeignKey(x => x.OrderId);
            });
        }
    }
}
