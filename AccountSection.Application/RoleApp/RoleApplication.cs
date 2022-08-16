using Common.Application;
using System.Collections.Generic;
using AccountSection.Domain.RoleAgg;
using AccountSection.Application.Contracts.RoleApp;

namespace AccountSection.Application.RoleApp
{
    public sealed class RoleApplication : 
        BaseEfApplication<IRoleRepo, SaveRole, SearchRole, ViewRole, Role>, IRoleApplication
    {
        #region Init
        public RoleApplication(IRoleRepo repo) : base(repo) { }
        #endregion

        public sealed override TaskResult Edit(SaveRole command)
        {
            TaskResult operation = new();
            var role = repo.Get(command.Id);

            if (role is null)
                return operation.Failed(TaskResult.Messages.RecordNotFound);

            if (repo.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(TaskResult.Messages.RecordIsDuplicated);

            var permissions = new List<Permission>();
            if (command.Permissions is null) command.Permissions = new();
            command.Permissions.ForEach(code => permissions.Add(new Permission(code)));

            role.Edit(command.Name, permissions);
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public sealed override TaskResult Create(SaveRole command)
        {
            TaskResult operation = new();

            if (repo.Exists(x => x.Name == command.Name))
                return operation.Failed(TaskResult.Messages.RecordIsDuplicated);

            var permissions = new List<Permission>();
            command.Permissions.ForEach(code => permissions.Add(new Permission(code)));

            Role New = new(command.Name, permissions);
            repo.Create(New);
            return operation.ReturnOkOnlyIfCommitted(repo.Save());
        }

        public bool HasPermission(HasPermissionDto command) => repo.HasPermission(command);

        public List<int> GetPermissionsByRoleId(long roleId) => repo.GetPermissionsByRoleId(roleId);
    }
}
