using Domain.Interfaces;

namespace Osiris.IdentityService.Infrastructure.Entities
{
    public class UserPermission : IEntityBase<Guid>, IDateAudited, IAuthorAudited<Guid?>
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid UserId { get; set; }
        public Guid ApplicationSystemFeatureId { get; set; }
        public bool CanCreate { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        #region Navigation Properties
        public virtual User? User { get; set; }
        public virtual ApplicationSystemFeature? ApplicationSystemFeature { get; set; }
        #endregion Navigation Properties
    }
}
