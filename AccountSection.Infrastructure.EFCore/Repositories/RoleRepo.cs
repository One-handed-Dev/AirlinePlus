using System;
using System.Linq;
using Common.Application;
using Common.Infrastructure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AccountSection.Domain.RoleAgg;
using AccountSection.Application.Contracts.RoleApp;

namespace AccountSection.Infrastructure.EFCore.Repositories
{
    public sealed class RoleRepo : BaseEfRepo<SaveRole, SearchRole, ViewRole, Role>, IRoleRepo
    {
        #region Init
        private readonly AccountContext context;

        public RoleRepo(AccountContext context) : base(context) => this.context = context;
        #endregion

        public new SaveRole GetDetails(long id)
        {
            var role = context.Roles.Select(x => new SaveRole
                {
                    Id = x.Id,
                    Name = x.Name,
                    MappedPermissions = MapPermissions(x.Permissions),
                    IsAllowedToEnterDashboard = x.IsAllowedToEnterDashboard
                })
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            role.Permissions = role?.MappedPermissions.Select(x => x.Code).ToList();

            return role;
        }

        public bool HasPermission(HasPermissionDto command) => new Lazy<bool>(()
            => context.Roles.Include(x => x.Permissions).SingleOrDefault(x => x.Id == command.RoleId)?.Permissions.Any(x => x.Code == command.Code) ?? false).Value;

        public sealed override List<ViewRole> Search(SearchRole command)
        {
            var query = Projection.FromList(new ViewRole(), context.Roles.AsNoTracking());

            if (command.IsRemoved) query = query.Where(x => x.IsRemoved);
            if (command.IsAllowedToEnterDashboard) query = query.Where(x => x.IsAllowedToEnterDashboard);
            if (!string.IsNullOrWhiteSpace(command.Name)) query = query.Where(x => x.Name.Contains(command.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<int> GetPermissionsByRoleId(long roleId) => Get(roleId).Permissions.Select(x => x.Code).ToList();

        private static List<PermissionDto> MapPermissions(List<Permission> permissions) => permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
    }
}
