using AuthorizeNetSample.BLL.Maps;
using AutoMapper;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AuthorizeNetSample.Web {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg => {
                cfg.AddProfile(new ServiceMapping());
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
