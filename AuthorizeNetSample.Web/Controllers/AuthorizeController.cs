using AuthorizeNetSample.Domain.Interfaces.Services;
using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Domain.Models.Dtos;
using AuthorizeNetSample.Web.Controllers.Base;
using AuthorizeNetSample.Web.Models;
using AuthorizeNetSample.Web.Models.Authorize;
using AutoMapper;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AuthorizeNetSample.Web.Controllers {
    public class AuthorizeController : BaseController
    {
        public AuthorizeController(IServiceHost serviceHost)
            : base(serviceHost) { }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetRequestToken() {
            AuthorizeConfigDto authorizeConfig = _serviceHost.GetService<IAuthorizeService>().GetConfig();
            if (authorizeConfig == null) return null;

            OauthClient client = _CreateAuthorizeOauthClient(authorizeConfig);
            if (client == null) return Redirect(nameof(Index));

            GetRequestTokenPageViewModel model = Mapper.Map<GetRequestTokenPageViewModel>(authorizeConfig);
            model.OAuthUrl = client.OauthUrl();
            model.Scope = client.GetScope();
            model.State = client.GetState();
            model.Sub = client.GetSub();

            return View(model);
        }

        [HttpPost]
        public RedirectResult GetRequestToken(GetRequestTokenPageViewModel model) {
            //string clientId = "4dp5b7gRqk";
            
            return Redirect(model.OAuthUrl);
        }

        [HttpGet]
        public ActionResult GetAccessToken(string clientId, string clientSecret) {
            GetAccessTokenPageViewModel model = new GetAccessTokenPageViewModel {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult GetAccessToken(GetAccessTokenPageViewModel model) {
            return Json("");
        }

        private OauthClient _CreateAuthorizeOauthClient(AuthorizeConfigDto authorizeConfig) {
            string requestTokenUrl = WebConfigurationManager.AppSettings["AuthorizeRequestTokenUrl"];

            OauthClient client = new OauthClient(requestTokenUrl, authorizeConfig.ClientId, authorizeConfig.RedirectUri, "test1023");
            return client;
        }
    }
}