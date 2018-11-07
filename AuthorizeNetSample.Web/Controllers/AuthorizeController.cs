using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using System;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AuthorizeNetSample.Web.Controllers {
    public class AuthorizeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public RedirectResult RequestToken() {
            string returnUrl = "https://localhost:17126/Authorize";
            //string OAuthUrl = WebConfigurationManager.AppSettings["AuthorizeRequestTokenEndpoint"];
            //string clientId = WebConfigurationManager.AppSettings["AuthorizeOAuthClientId"];
            string OAuthUrl = "https://sandbox.authorize.net/oauth/authorize";
            string clientId = "4dp5b7gRqk";
            //IOAuthSession session = CreateSession();

            //if (session == null)
            //    return Redirect(returnUrl);

            //IToken requestToken;

            //try {
            //    requestToken = session.GetRequestToken();
            //} catch (Exception e) {
            //    return Redirect(returnUrl);
            //}

            //Session["RequestToken"] = requestToken;
            //Session["Token"] = requestToken.Token;
            //Session["TokenSecret"] = requestToken.TokenSecret;
            //var authUrl = $"{QbSettings.AuthUrl}?oauth_token={requestToken.Token}" +
            //              $"&oauth_callback={UriUtility.UrlEncode(urlHelper.Action("GetAccessToken", "QuickBooks", null, "https"))}";
            //Session["UrlBack"] = returnUrl;
            //return Redirect(authUrl);
            OAuthUrl = OAuthUrl + $"?sub=oauth&client_id={clientId}&state=1&scope=read,write&redirect_uri={returnUrl}";
            return Redirect(OAuthUrl);
        }

        private IOAuthSession CreateSession() {
            string OAuthConsumerKey = WebConfigurationManager.AppSettings["AuthorizeApiLoginId"];
            string OAuthConsumerSecret = WebConfigurationManager.AppSettings["AuthorizeTransactionKey"];
            string ServiceProvider = WebConfigurationManager.AppSettings["AuthorizeRequestTokenEndpoint"];
            string OAuthLink = WebConfigurationManager.AppSettings["AuthorizeOauth"];
            string AccessToken = WebConfigurationManager.AppSettings["AuthorizeAccessTokenEndpoint"];

            OAuthConsumerContext consumerContext = new OAuthConsumerContext {
                ConsumerKey = OAuthConsumerKey,
                ConsumerSecret = OAuthConsumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1
            };

            return new OAuthSession(consumerContext, ServiceProvider, OAuthLink, AccessToken);
        }
    }
}