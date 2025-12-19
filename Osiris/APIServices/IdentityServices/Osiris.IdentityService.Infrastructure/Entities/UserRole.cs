using Microsoft.AspNetCore.Identity;


namespace Osiris.IdentityService.Infrastructure.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        #region Navigation Properties
        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
        #endregion Navigation Properties
    }
}
