using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models {
    public class GetAccessTokenPageViewModel {
        [DisplayName("Client Id")]
        public string ClientId { get; set; }
        [DisplayName("Client Secret")]
        public string ClientSecret { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
    }
}