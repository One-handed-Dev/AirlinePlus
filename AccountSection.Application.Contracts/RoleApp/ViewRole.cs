using Common.Application.Contracts;
using Common.Domain;

namespace AccountSection.Application.Contracts.RoleApp
{
    public sealed class ViewRole : BaseEfViewModel, ICreation<string>
    {
        public bool IsAllowedToEnterDashboard { get; set; }
    }
}
