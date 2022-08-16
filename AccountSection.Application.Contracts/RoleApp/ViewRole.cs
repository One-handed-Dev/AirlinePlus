using Common.Domain;
using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.RoleApp
{
    public sealed class ViewRole : BaseEfViewModel, ICreation<string>
    {
        public bool IsAllowedToEnterDashboard { get; set; }
    }
}
