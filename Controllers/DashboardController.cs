using BalajiPortfolio.Helpers.DbContexts;
using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio.Controllers
{
    public class DashboardController : Controller
    {
        #region Constructor and Dependency Injection
        private readonly appDBContext _context;

        public DashboardController(appDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Page Load Functions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var personal = await _context.mstPersonals.FirstOrDefaultAsync(p => p.isActive);

            var model = new DashboardDTO
            {
                TotalProjects = await _context.mstProjects.CountAsync(p => p.isActive),
                TotalSkills = await _context.mstSkills.CountAsync(s => s.isActive),
                TotalCertifications = await _context.mstCertifications.CountAsync(c => c.isActive),
                TotalPublications = await _context.mstPublications.CountAsync(p => p.isActive),
                TotalExperiences = await _context.mstExperiences.CountAsync(e => e.isActive),
                ProfileName = personal?.name,
                HasProfile = personal != null && !string.IsNullOrWhiteSpace(personal.name)
            };

            return View("~/Views/Dashboard/dashboard.cshtml", model);
        }
        #endregion
    }
}
