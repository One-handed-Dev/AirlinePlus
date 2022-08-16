using Microsoft.EntityFrameworkCore;
using CommentSection.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentSection.Infrastructure.EFCore.Mappings
{
    public sealed class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).HasMaxLength(2000);
            builder.Property(x => x.Feedback).HasMaxLength(2000);
        }
    }
}
