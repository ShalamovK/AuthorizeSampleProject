using Newtonsoft.Json;
using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models.Authorize {
    public class AccessTokenResponse {
        [JsonProperty("access_token")]
        [DisplayName("Access Token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        [DisplayName("Refresh Token")]
        public string RefreshToken { get; set; }
        [JsonProperty("token_type")]
        [DisplayName("Token Type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        [DisplayName("Access Token Expires In(seconds)")]
        public int ExpiresIn { get; set; }
        [JsonProperty("scope")]
        [DisplayName("Scope")]
        public string Scope { get; set; }
        [JsonProperty("refresh_token_expires_in")]
        [DisplayName("Refresh Token Expires In(seconds)")]
        public int RefreshTokenExpiresIn { get; set; }
        [JsonProperty("client_status")]
        [DisplayName("Client Status")]
        public string ClientStatus { get; set; }
    }
}