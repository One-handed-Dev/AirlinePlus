using Common.Application;
using Common.Domain;

namespace AccountSection.Domain.ForgetPasswordLogAgg
{
    public class ForgetPasswordLog : BaseEfDomainModel
    {
        public bool IsUsed { get; set; }
        public string Hash { get; set; }
        public bool IsExpired { get; set; }
        public string PhoneNumber { get; set; }

        public void Use()
        {
            IsUsed = true;
            IsExpired = true;
        }

        public override void Edit<T>(T edited) => this.From(edited);
    }
}
