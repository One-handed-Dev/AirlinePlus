using System;

namespace Common.Application.Contracts
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NeedsPermissionAttribute : Attribute
    {
        public int Permission { get; set; }

        public NeedsPermissionAttribute(int permission) => Permission = permission;
    }
}
