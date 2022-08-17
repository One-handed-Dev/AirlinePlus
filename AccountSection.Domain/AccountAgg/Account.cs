using AccountSection.Domain.RoleAgg;
using Common.Application;
using Common.Domain;
using static Common.Application.Projection;

namespace AccountSection.Domain.AccountAgg
{
    public class Account : BaseEfDomainModel
    {
        public Role Role { get; set; }
        public long RoleId { get; set; }
        public string Email { get; set; }
        public bool IsBanned { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public long ModifierAccountId { get; set; }
        public string VerificationCode { get; set; }

        #region Methods
        public void Ban() => IsBanned = true;

        public void Unban() => IsBanned = false;

        public void ChangeRole(long newRoleId, long modifierAccountId)
        {
            RoleId = newRoleId;
            ModifierAccountId = modifierAccountId;
        }

        public void ChangePassword(string password) => Password = password;

        public sealed override void Edit<T>(T edited) => this.From(edited, CalendarMode.ToGregorian, NullOrEmptyPicture.Ignore);
        #endregion
    }
}
