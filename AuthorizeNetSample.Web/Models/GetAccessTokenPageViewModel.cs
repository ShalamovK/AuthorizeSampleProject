namespace AuthorizeNetSample.Web.Models {
    public class GetAccessTokenPageViewModel {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Code { get; set; }
    }
}