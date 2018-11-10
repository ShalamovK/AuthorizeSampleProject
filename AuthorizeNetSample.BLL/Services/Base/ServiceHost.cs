using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using Unity;

namespace AuthorizeNetSample.BLL.Services.Base {
    public class ServiceHost : IServiceHost {
        private readonly IUnityContainer _container;
        private readonly Dictionary<Type, IService> _services;

        public ServiceHost(IUnityContainer container) {
            _container = container;
            _services = new Dictionary<Type, IService>();
        }

        public void Register<T>(T service) where T : IService {
            if (!_services.ContainsKey(typeof(T))) {
                _services.Add(typeof(T), service);
            }
        }

        public T GetService<T>() where T : IService {
            if (_services.ContainsKey(typeof(T))) {
                return (T)_services[typeof(T)];
            }

            var service = _container.Resolve<T>();
            Register(service);

            return service;
        }
    }
}
