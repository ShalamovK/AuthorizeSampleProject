namespace AuthorizeNetSample.Web.Models.Authorize {
    public class OauthClient {
        private readonly string _oauthUrl;
        private readonly string _clientId;
        private readonly string _redirectUri;
        private readonly string _state;
        private readonly string _scope;
        private readonly string _sub;

        public OauthClient(string oauthUrl, string clientId, string redirectUri, string state) {
            _clientId = clientId;
            _redirectUri = redirectUri;
            _state = state;
            _oauthUrl = oauthUrl;

            _scope = "read,write";
            _sub = "oauth";
        }

        public string GetState() {
            return _state;
        }

        public string GetScope() {
            return _scope;
        }

        public string GetSub() {
            return _sub;
        }

        public string OauthUrl() {
            return $"{_oauthUrl}?client_id={_clientId}&redirect_uri={_redirectUri}&scope={_scope}&state={_state}&sub={_sub}";
        }
    }
}