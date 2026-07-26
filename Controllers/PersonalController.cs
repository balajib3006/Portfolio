using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class PersonalController : Controller
    {
        #region Constructor and Dependency Injection
        private readonly IPersonalRepository _personalRepository;
        private readonly IApiResponseRepository _apiResponseRepository;

        public PersonalController(
            IPersonalRepository personalRepository,
            IApiResponseRepository apiResponseRepository)
        {
            _personalRepository = personalRepository;
            _apiResponseRepository = apiResponseRepository;
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var personalData = await _personalRepository.GetPersonalDetailsAsync() ?? new PersonalDTO();
            return View("~/Views/Personal/personal.cshtml", personalData);
        }
        #endregion

        #region Personal - API Endpoints
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDetails(PersonalDTO personalDto, IFormFile? profilePictureFile)
        {
            var response = await _personalRepository.SavePersonalDetailsAsync(personalDto, profilePictureFile);
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
        public async Task<IActionResult> SaveAbout(PersonalDTO personalDto)
        {
            var response = await _personalRepository.SaveAboutSectionAsync(personalDto);
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
        public async Task<IActionResult> SaveSocialMedia(PersonalDTO personalDto)
        {
            var response = await _personalRepository.SaveSocialMediaAsync(personalDto);
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
