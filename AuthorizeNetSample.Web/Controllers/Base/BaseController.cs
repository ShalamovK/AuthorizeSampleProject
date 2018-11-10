using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using System;
using System.Web.Mvc;

namespace AuthorizeNetSample.Web.Controllers.Base {
    public class BaseController : Controller {
        protected readonly IServiceHost _serviceHost;

        public BaseController(IServiceHost serviceHost) {
            _serviceHost = serviceHost ?? throw new ArgumentNullException("serviceHost");
        }
    }
}