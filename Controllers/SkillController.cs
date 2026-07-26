using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class SkillController : Controller
    {
        #region Constructor and Dependency Injection
        public SkillController()
        {
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Skills/skills.cshtml");
        }
        #endregion
    }
}
