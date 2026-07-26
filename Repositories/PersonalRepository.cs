using BalajiPortfolio.Data;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio.Repositories
{
    public class PersonalRepository : IPersonalRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<mstPersonal?> GetPersonalAsync()
        {
            return await _context.MstPersonals.FirstOrDefaultAsync();
        }

        public async Task SavePersonalDetailsAsync(mstPersonal model)
        {
            var existing = await _context.MstPersonals.FirstOrDefaultAsync();
            if (existing == null)
            {
                model.UpdatedAt = DateTime.UtcNow;
                await _context.MstPersonals.AddAsync(model);
            }
            else
            {
                existing.Name = model.Name;
                if (!string.IsNullOrEmpty(model.ProfilePictureUrl))
                {
                    existing.ProfilePictureUrl = model.ProfilePictureUrl;
                }
                existing.MobileNumber = model.MobileNumber;
                existing.Email = model.Email;
                existing.Location = model.Location;
                existing.Bio = model.Bio;
                existing.WorkDetails = model.WorkDetails;
                existing.UpdatedAt = DateTime.UtcNow;
                _context.MstPersonals.Update(existing);
            }
            await _context.SaveChangesAsync();
        }

        public async Task SaveAboutSectionAsync(mstPersonal model)
        {
            var existing = await _context.MstPersonals.FirstOrDefaultAsync();
            if (existing == null)
            {
                model.UpdatedAt = DateTime.UtcNow;
                await _context.MstPersonals.AddAsync(model);
            }
            else
            {
                existing.ProfessionalSummary = model.ProfessionalSummary;
                existing.YearsOfExperience = model.YearsOfExperience;
                existing.ProjectsCompleted = model.ProjectsCompleted;
                existing.CompaniesWorked = model.CompaniesWorked;
                existing.UpdatedAt = DateTime.UtcNow;
                _context.MstPersonals.Update(existing);
            }
            await _context.SaveChangesAsync();
        }

        public async Task SaveSocialMediaAsync(mstPersonal model)
        {
            var existing = await _context.MstPersonals.FirstOrDefaultAsync();
            if (existing == null)
            {
                model.UpdatedAt = DateTime.UtcNow;
                await _context.MstPersonals.AddAsync(model);
            }
            else
            {
                existing.LinkedInUrl = model.LinkedInUrl;
                existing.GitHubUrl = model.GitHubUrl;
                existing.GitLabUrl = model.GitLabUrl;
                existing.OrcidUrl = model.OrcidUrl;
                existing.GoogleScholarUrl = model.GoogleScholarUrl;
                existing.UpdatedAt = DateTime.UtcNow;
                _context.MstPersonals.Update(existing);
            }
            await _context.SaveChangesAsync();
        }
    }
}
