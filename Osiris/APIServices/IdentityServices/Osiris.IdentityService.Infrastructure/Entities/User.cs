using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Osiris.IdentityService.Infrastructure.Entities
{
    public class User : IdentityUser<Guid>, IEntityBase<Guid>, IDateAudited, IAuthorAudited<Guid?>, ISoftDeleteAudited
    {
        public string? DisplayName { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsGoogleAccountLinked { get; set; }
        public string? StripeUserId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        #region Navigation Properties
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<LoginSession>? LoginSessions { get; set; }
        public virtual ICollection<OneTimeVerificationToken>? OneTimeVerificationTokens { get; set; }
        public virtual ICollection<UserPermission>? UserPermissions { get; set; }
        #endregion Navigation Properties

    }
}
