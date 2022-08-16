namespace Common.Infrastructure
{
    public static class DefinedRoles
    {
        public static string Administrator { get; } = "1";
        public static string Customer { get; } = "2";

        public static string GetRoleBy(long id) => id switch
        {
            1 => "مدیر سیستم",
            2 => "مشتری",
            _ => string.Empty,
        };
    }
}
