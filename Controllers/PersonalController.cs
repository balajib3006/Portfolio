using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class PersonalController : Controller
    {
        private readonly IPersonalRepository _personalRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PersonalController(IPersonalRepository personalRepository, IWebHostEnvironment webHostEnvironment)
        {
            _personalRepository = personalRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var personalData = await _personalRepository.GetPersonalAsync() ?? new mstPersonal();
            return View("~/Views/Personal/personal.cshtml", personalData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDetails(mstPersonal model, IFormFile? profilePictureFile)
        {
            if (profilePictureFile != null && profilePictureFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profile");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(profilePictureFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePictureFile.CopyToAsync(fileStream);
                }

                model.ProfilePictureUrl = "/uploads/profile/" + uniqueFileName;
            }

            await _personalRepository.SavePersonalDetailsAsync(model);
            TempData["SuccessMessage"] = "Personal details saved successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAbout(mstPersonal model)
        {
            await _personalRepository.SaveAboutSectionAsync(model);
            TempData["SuccessMessage"] = "About section saved successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSocialMedia(mstPersonal model)
        {
            await _personalRepository.SaveSocialMediaAsync(model);
            TempData["SuccessMessage"] = "Social media links saved successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
