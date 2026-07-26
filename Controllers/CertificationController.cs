using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class CertificationController : Controller
    {
        #region Constructor and Dependency Injection
        public CertificationController()
        {
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Certifications/certifications.cshtml");
        }
        #endregion
    }
}
