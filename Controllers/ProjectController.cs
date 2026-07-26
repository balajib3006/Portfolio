using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BalajiPortfolio.Controllers
{
    public class ProjectController : Controller
    {
        #region Constructor and Dependency Injection
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return View("~/Views/Projects/projects.cshtml", projects);
        }
        #endregion

        #region Project - API Endpoints
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProject(ProjectDTO dto, IFormFile? productImageFile, IFormFile? cadFile)
        {
            var response = await _projectRepository.SaveProjectAsync(dto, productImageFile, cadFile);
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
        public async Task<IActionResult> DeleteProject(int id)
        {
            var response = await _projectRepository.DeleteProjectAsync(id);
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
