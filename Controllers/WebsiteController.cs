using BalajiPortfolio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class WebsiteController : Controller
    {
        #region Dependency Injection
        private readonly IWebsiteRepository _websiteRepository;

        public WebsiteController(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }
        #endregion

        #region Actions
        [HttpGet]
        [Route("portfolio")]
        [Route("Website/Portfolio")]
        [Route("Website/Index")]
        [Route("Website")]
        public async Task<IActionResult> Portfolio()
        {
            var portfolioData = await _websiteRepository.GetPortfolioDataAsync();
            return View("~/Views/Website/portfolio.cshtml", portfolioData);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await Portfolio();
        }
        #endregion
    }
}
