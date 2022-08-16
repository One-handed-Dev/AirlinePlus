using Common.Domain;
using WebQuery.Query;
using WebQuery.Contracts.Comment;
using Microsoft.EntityFrameworkCore;
using CommentSection.Domain.CommentAgg;
using CommentSection.Infrastructure.EFCore;
using CommentSection.Application.CommentApp;
using Microsoft.Extensions.DependencyInjection;
using CommentSection.Application.Contracts.CommentApp;
using CommentSection.Infrastructure.Config.Permissions;
using CommentSection.Infrastructure.EFCore.Repositories;

namespace CommentSection.Infrastructure.Config
{
    public sealed class CommentSectionConfig
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));

            services.AddTransient<ICommentQuery, CommentQuery>();
            services.AddTransient<IPermissionExposer, CommentPermissionExposer>();

            services.AddTransient<ICommentRepo, CommentRepo>(); services.AddTransient<ICommentApplication, CommentApplication>();
        }
    }
}
