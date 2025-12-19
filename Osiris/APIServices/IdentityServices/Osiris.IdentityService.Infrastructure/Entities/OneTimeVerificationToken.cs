using Domain.Enums;
using Domain.Interfaces;

namespace Osiris.IdentityService.Infrastructure.Entities
{
    public class OneTimeVerificationToken : IDateAudited
    {
        public Guid UserId { get; set; }
        public OneTimeAuthAction Action { get; set; }
        public TokenType Type { get; set; }
        public required string Session { get; set; }
        public required string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsUsed { get; set; }

        #region Navigation Properties
        public virtual User? User { get; set; }
        #endregion Navigation Properties
    }
}
