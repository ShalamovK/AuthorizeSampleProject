using AuthorizeNetSample.BLL.Services;
using AuthorizeNetSample.BLL.Services.Base;
using AuthorizeNetSample.Domain.Interfaces.Services;
using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Repositories;
using AuthorizeNetSample.Web.Ioc;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.Injection;

namespace AuthorizeNetSample.Web {
    public static class UnityConfig {
        private static IUnityContainer _container;

        public static IUnityContainer Container {
            get {
                if (_container == null) {
                    _container = BuildUnityContainer();
                }
                return _container;
            }
        }

        public static void RegisterComponents(HttpConfiguration config) {
            config.DependencyResolver = new UnityResolver(Container);
        }

        private static IUnityContainer BuildUnityContainer() {
            var container = new UnityContainer();

            // DAL
            var connectionString = ConfigurationManager.ConnectionStrings["AuthorizeDbConnection"].ConnectionString;
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestOrTransientLifeTimeManager());

            // BLL
            container.RegisterType<IServiceHost, ServiceHost>(new PerRequestOrTransientLifeTimeManager(), new InjectionConstructor(container));
            container.RegisterType<IAuthorizeService, AuthorizeService>();

            return container;
        }
    }
}