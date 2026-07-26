using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class CertificationController : Controller
    {
        #region Constructor and Dependency Injection
        private readonly ICertificationRepository _certificationRepository;

        public CertificationController(ICertificationRepository certificationRepository)
        {
            _certificationRepository = certificationRepository;
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var certifications = await _certificationRepository.GetAllCertificationsAsync();
            return View("~/Views/Certifications/certifications.cshtml", certifications);
        }
        #endregion

        #region Certification - API Endpoints
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCertification(CertificationDTO dto)
        {
            var response = await _certificationRepository.SaveCertificationAsync(dto);
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
        public async Task<IActionResult> DeleteCertification(int id)
        {
            var response = await _certificationRepository.DeleteCertificationAsync(id);
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
