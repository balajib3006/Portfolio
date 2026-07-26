using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class PublicationController : Controller
    {
        #region Constructor and Dependency Injection
        public PublicationController()
        {
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Publications/publications.cshtml");
        }
        #endregion
    }
}
