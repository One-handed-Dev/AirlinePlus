using CommentSection.Domain.CommentAgg;
using CommentSection.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CommentSection.Infrastructure.EFCore
{
    public sealed class CommentContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public CommentContext(DbContextOptions<CommentContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
