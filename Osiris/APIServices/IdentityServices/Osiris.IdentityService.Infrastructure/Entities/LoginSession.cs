namespace Osiris.IdentityService.Infrastructure.Entities
{
    public class LoginSession
    {
        public Guid UserId { get; set; }
        public string? IpAddress { get; set; }
        public required string AccessToken { get; set; }
        public DateTime? AccessTokenExpirationDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
        public bool IsUsedRefreshToken { get; set; }
        /// <summary>
        /// When session started
        /// </summary>
        public DateTime? StartedDate { get; set; }
        /// <summary>
        /// When session ended, If it is not null, it means the session has ended or revoked.
        /// </summary>
        public DateTime? EndedDate { get; set; }

        #region Navigation Properties
        public virtual User? User { get; set; }
        #endregion  Navigation Properties
    }
}
