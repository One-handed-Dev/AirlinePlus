using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.AccountApp
{
    public class SearchAccount : BaseEfSearchModel
    {
        public long RoleId { get; set; }
        public string Email { get; set; }
        public bool IsBanned { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
