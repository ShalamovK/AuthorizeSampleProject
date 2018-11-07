using System;

namespace AuthorizeNetSample.Domain.Models.Dtos {
    public class AuthorizeConfigDto {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RedirectUri { get; set; }
        public DateTime? AccesssTokenExpiresIn { get; set; }
        public DateTime? RefreshTokenExpiresIn { get; set; }
    }
}
