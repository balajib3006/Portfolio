using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class ExperienceController : Controller
    {
        #region Constructor and Dependency Injection
        public ExperienceController()
        {
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Experiences/experiences.cshtml");
        }
        #endregion
    }
}
