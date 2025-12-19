using Domain.Enums;
using Domain.Interfaces;

namespace Osiris.IdentityService.Infrastructure.Entities
{
    public class ApplicationSystemFeature : IEntityBase<Guid>
    {
        public Guid Id { get; set; }
        public Guid ApplicationSystemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public FeatureType Type { get; set; }

        #region Navigation Properties
        public virtual ApplicationSystem? ApplicationSystem { get; set; }
        public virtual ICollection<RolePermission>? RolePermissions { get; set; }
        public virtual ICollection<UserPermission>? UserPermissions { get; set; }
        #endregion Navigation Properties
    }
}
