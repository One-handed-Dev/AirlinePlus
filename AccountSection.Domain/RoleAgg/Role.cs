using AccountSection.Domain.AccountAgg;
using Common.Domain;
using System.Collections.Generic;

namespace AccountSection.Domain.RoleAgg
{
    public sealed class Role : BaseEfDomainModel
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Permission> Permissions { get; set; }
        public bool IsAllowedToEnterDashboard { get; set; }

        public Role() { }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
            Accounts = new List<Account>();
        }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
