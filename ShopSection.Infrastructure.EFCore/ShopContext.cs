using ShopSection.Domain.OrderAgg;
using ShopSection.Domain.FlightAgg;
using Microsoft.EntityFrameworkCore;
using ShopSection.Domain.AirlineAgg;
using ShopSection.Infrastructure.EFCore.Mappings;

namespace ShopSection.Infrastructure.EFCore
{
    public class ShopContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airline> Airlines { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assemlby = typeof(AirlineMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemlby);
        }
    }
}
