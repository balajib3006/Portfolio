using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class ProjectController : Controller
    {
        #region Constructor and Dependency Injection
        public ProjectController()
        {
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Projects/projects.cshtml");
        }
        #endregion
    }
}
