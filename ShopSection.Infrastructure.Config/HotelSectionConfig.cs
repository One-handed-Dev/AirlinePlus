using Common.Domain;
using WebQuery.Query;
using ShopSection.Domain.OrderAgg;
using Microsoft.EntityFrameworkCore;
using WebQuery.Contracts.Shop.Order;
using ShopSection.Domain.ServiceAgg;
using ShopSection.Application.OrderApp;
using ShopSection.Infrastructure.EFCore;
using ShopSection.Application.ServiceApp;
using Microsoft.Extensions.DependencyInjection;
using ShopSection.Application.Contracts.OrderApp;
using ShopSection.Infrastructure.Config.Permissions;
using ShopSection.Infrastructure.EFCore.Repositories;

namespace ShopSection.Infrastructure.Config
{
    public class ShopSectionConfig
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

            services.AddTransient<IOrderQuery, OrderQuery>();
            services.AddSingleton<ICartFetchService, CartFetchService>();
            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();
            services.AddTransient<ICartCalculateService, CartCalculateService>();

            services.AddTransient<IOrderRepo, OrderRepo>(); services.AddTransient<IOrderApplication, OrderApplication>();
        }
    }
}
