using Domain.Interfaces;

namespace Osiris.IdentityService.Infrastructure.Entities
{
    public class ApplicationSystem : IEntityBase<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } // Active or Inactive

        #region Navigation Properties
        public virtual ICollection<ApplicationSystemFeature>? ApplicationSystemFeatures { get; set; }
        #endregion Navigation Properties
    }
}
