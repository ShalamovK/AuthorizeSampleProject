using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models {
    public class GetRequestTokenPageViewModel {
        [DisplayName("Client_Id")]
        public string ClientId { get; set; }
        [DisplayName("Client_Secret")]
        public string ClientSecret { get; set; }
        [DisplayName("Refresh Token")]
        public string RefreshToken { get; set; }
        [DisplayName("Redirect URI")]
        public string RedirectUri { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("Scope")]
        public string Scope { get; set; }
        [DisplayName("Sub")]
        public string Sub { get; set; }
        [DisplayName("Get Request Token URL")]
        public string OAuthUrl { get; set; }
    }
}