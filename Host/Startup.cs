using Host.Classes;
using System.Text.Unicode;
using System.IO.Compression;
using Common.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ShopSection.Infrastructure.Config;
using Microsoft.Extensions.Configuration;
using InteractionSection.Domain.EmailAgg;
using AccountSection.Infrastructure.Config;
using CommentSection.Infrastructure.Config;
using Microsoft.Extensions.DependencyInjection;
using InteractionSection.Infrastructure.Config;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Host
{
    public class Startup
    {
        #region Init
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;
        #endregion

        public void ConfigureServices(IServiceCollection services)
        {
            var conncetionString = Configuration.GetConnectionString("AirlinePlusDb");

            HostConfig.Configure(services);
            ShopSectionConfig.Configure(services, conncetionString);
            CommentSectionConfig.Configure(services, conncetionString);
            AccountSectionConfig.Configure(services, conncetionString);
            InteractionSectionConfig.Configure(services, conncetionString);
            services.AddSingleton(Configuration.GetSection("EmailConfigurationDto").Get<EmailConfigurationDto>());

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.AddCors(o => o.AddPolicy("All", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.Configure<CookiePolicyOptions>(o =>
            {
                o.CheckConsentNeeded = context => false;
                o.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/LogIn");
                    o.LogoutPath = new PathString("/LogIn");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("AdminAreaFullAccess", builder => builder.RequireRole(new List<string> { DefinedRoles.Administrator }));
            });

            services.AddRazorPages()
                .AddMvcOptions(o => o.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(o =>
                {
                    o.Conventions.AuthorizeAreaFolder("Dashboard", "/", "AdminAreaFullAccess");
                })
                ;//.AddApplicationPart(typeof(ReserveHotelController).Assembly);

            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddAntiforgery(x => x.HeaderName = "XSRF-TOKEN");
            services.AddResponseCompression(x => x.EnableForHttps = true);
            services.Configure<GzipCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthorization();
            app.UseCors("All");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
