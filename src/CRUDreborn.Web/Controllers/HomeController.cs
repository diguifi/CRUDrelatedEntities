using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace CRUDreborn.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : CRUDrebornControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}