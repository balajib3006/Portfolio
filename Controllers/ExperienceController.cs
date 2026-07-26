using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class ExperienceController : Controller
    {
        #region Constructor and Dependency Injection
        private readonly IExperienceRepository _experienceRepository;

        public ExperienceController(IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var experiences = await _experienceRepository.GetAllExperiencesAsync();
            return View("~/Views/Experiences/experiences.cshtml", experiences);
        }
        #endregion

        #region Experience - API Endpoints
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveExperience(ExperienceDTO dto)
        {
            var response = await _experienceRepository.SaveExperienceAsync(dto);
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
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var response = await _experienceRepository.DeleteExperienceAsync(id);
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
