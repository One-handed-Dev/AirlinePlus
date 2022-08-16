using Microsoft.EntityFrameworkCore;
using InteractionSection.Domain.EmailAgg;
using InteractionSection.Infrastructure.EFCore.Mappings;

namespace InteractionSection.Infrastructure.EFCore
{
    public class InteractionContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }

        public InteractionContext(DbContextOptions<InteractionContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assemlby = typeof(EmailMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemlby);
        }
    }
}
