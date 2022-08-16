namespace AccountSection.Domain.RoleAgg
{
    public sealed class Permission
    {
        public long Id { get; set; }
        public int Code { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public long RoleId { get; set; }

        public Permission(int code) => Code = code;

        public Permission(int code, string name) : this(code) => Name = name;
    }
}
