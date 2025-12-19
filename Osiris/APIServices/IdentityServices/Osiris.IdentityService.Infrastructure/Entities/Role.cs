using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Osiris.IdentityService.Infrastructure.Entities
{
    public class Role : IdentityRole<Guid>, IEntityBase<Guid>
    {
        #region Navigation Properties
        public ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<RolePermission>? RolePermissions { get; set; }
        #endregion Navigation Properties
    }
}
