using Common.Infrastructure;
using System.Collections.Generic;

namespace Common.Domain
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}
