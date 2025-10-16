namespace Shared.Models.ConfigurationModels
{
    public class JwtTokenConfig
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int? AccessTokenExpirationInMinutes { get; set; }
        public int? RefreshTokenExpirationInMinutes { get; set; }
    }
}
