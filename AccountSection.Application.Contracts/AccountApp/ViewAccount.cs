using Common.Application.Contracts;

namespace AccountSection.Application.Contracts.AccountApp
{
    public class ViewAccount : BaseEfViewModel
    {
        public string Role { get; set; }
        public long RoleId { get; set; }
        public string Email { get; set; }
        public bool IsBanned { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
    }
}
