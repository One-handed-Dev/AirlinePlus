using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.RoleApp
{
    public sealed class SearchRole : BaseEfSearchModel
    {
        public bool IsAllowedToEnterDashboard { get; set; }
    }
}
