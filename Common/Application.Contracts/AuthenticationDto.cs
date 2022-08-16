using System.Collections.Generic;

namespace Common.Application.Contracts
{
    public sealed class AuthenticationDto
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> Permissions { get; set; }

        public AuthenticationDto() { }

        public AuthenticationDto(long id, long roleId, string fullname, string phoneNumber, List<int> permissions)
        {
            Id = id;
            RoleId = roleId;
            Fullname = fullname;
            PhoneNumber = phoneNumber;
            Permissions = permissions;
        }
    }
}