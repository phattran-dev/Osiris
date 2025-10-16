using Shared.Enums;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Constants
{
    public static class SeedRoles
    {
        public static readonly FrozenDictionary<RoleType, Guid> RoleIds = new Dictionary<RoleType, Guid>
        {
            { RoleType.Admin, Guid.Parse(SeedIds.Admin) },
            { RoleType.Staff, Guid.Parse(SeedIds.Staff) },
            { RoleType.User, Guid.Parse(SeedIds.User) }
        }.ToFrozenDictionary();
    }
}
