using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class PublicationController : Controller
    {
        #region Constructor and Dependency Injection
        private readonly IPublicationRepository _publicationRepository;

        public PublicationController(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var publications = await _publicationRepository.GetAllPublicationsAsync();
            return View("~/Views/Publications/publications.cshtml", publications);
        }
        #endregion

        #region Publication - API Endpoints
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePublication(PublicationDTO dto)
        {
            var response = await _publicationRepository.SavePublicationAsync(dto);
            if (response.success)
            {
                TempData["SuccessMessage"] = response.message;
            }
            else
            {
                TempData["ErrorMessage"] = response.message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePublication(int id)
        {
            var response = await _publicationRepository.DeletePublicationAsync(id);
            if (response.success)
            {
                TempData["SuccessMessage"] = response.message;
            }
            else
            {
                TempData["ErrorMessage"] = response.message;
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
