using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class DashboardController : Controller
    {
        #region Constructor and Dependency Injection
        public DashboardController()
        {
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Dashboard/dashboard.cshtml");
        }
        #endregion
    }
}
