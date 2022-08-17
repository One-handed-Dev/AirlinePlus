using AccountSection.Domain.AccountAgg;
using AccountSection.Domain.ForgetPasswordLogAgg;
using AccountSection.Domain.RoleAgg;
using AccountSection.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AccountSection.Infrastructure.EFCore
{
    public class AccountContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ForgetPasswordLog> ForgetPasswordLogs { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assemlby = typeof(AccountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemlby);
        }
    }
}
