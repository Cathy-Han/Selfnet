using Microsoft.AspNetCore.Mvc;

namespace Selfnet.Web.Controllers
{
    public class HomeController : SelfnetControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}