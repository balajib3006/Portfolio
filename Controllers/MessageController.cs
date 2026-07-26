using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class MessageController : Controller
    {
        #region Constructor and Dependency Injection
        public MessageController()
        {
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Messages/messages.cshtml");
        }
        #endregion
    }
}
