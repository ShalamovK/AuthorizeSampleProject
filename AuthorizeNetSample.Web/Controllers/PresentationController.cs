using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Web.Controllers.Base;
using System.Web.Mvc;

namespace AuthorizeNetSample.Web.Controllers {
    public class PresentationController : BaseController
    {
        public PresentationController(IServiceHost serviceHost)
            : base(serviceHost) { }
        public ActionResult Title()
        {
            return View();
        }
    }
}