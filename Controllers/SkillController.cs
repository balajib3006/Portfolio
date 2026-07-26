using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class SkillController : Controller
    {
        #region Constructor and Dependency Injection
        private readonly ISkillRepository _skillRepository;

        public SkillController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _skillRepository.GetSkillsPageDataAsync();
            return View("~/Views/Skills/skills.cshtml", data);
        }
        #endregion

        #region Skill Category - API Endpoints
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCategory(SkillCategoryDTO dto)
        {
            var response = await _skillRepository.SaveCategoryAsync(dto);
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _skillRepository.DeleteCategoryAsync(id);
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

        #region Skill - API Endpoints
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSkill(SkillDTO dto)
        {
            var response = await _skillRepository.SaveSkillAsync(dto);
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
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var response = await _skillRepository.DeleteSkillAsync(id);
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
