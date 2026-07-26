using BalajiPortfolio.Helpers.DbContexts;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using BalajiPortfolio.Models.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio.Repositories
{
    public class PersonalRepository : IPersonalRepository
    {
        #region DB Context and Interface References
        private readonly appDBContext _context;
        private readonly IApiResponseRepository _apiResponseRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PersonalRepository(
            appDBContext context,
            IApiResponseRepository apiResponseRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _apiResponseRepository = apiResponseRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Personal - Query Operations
        public async Task<PersonalDTO?> GetPersonalDetailsAsync()
        {
            return await _context.mstPersonals
                .Where(p => p.isActive)
                .Select(p => new PersonalDTO
                {
                    mstPersonalId = p.mstPersonalId,
                    name = p.name,
                    profilePictureUrl = p.profilePictureUrl,
                    mobileNumber = p.mobileNumber,
                    email = p.email,
                    location = p.location,
                    bio = p.bio,
                    workDetails = p.workDetails,
                    professionalSummary = p.professionalSummary,
                    yearsOfExperience = p.yearsOfExperience,
                    projectsCompleted = p.projectsCompleted,
                    companiesWorked = p.companiesWorked,
                    linkedInUrl = p.linkedInUrl,
                    gitHubUrl = p.gitHubUrl,
                    gitLabUrl = p.gitLabUrl,
                    orcidUrl = p.orcidUrl,
                    googleScholarUrl = p.googleScholarUrl,
                    isActive = p.isActive,
                    updatedAt = p.updatedAt
                })
                .FirstOrDefaultAsync();
        }
        #endregion

        #region Personal - CRUD Operations
        public async Task<ApiResponseDTO> SavePersonalDetailsAsync(PersonalDTO personalDto, IFormFile? profilePictureFile)
        {
            try
            {
                var existingEntity = await _context.mstPersonals
                    .FirstOrDefaultAsync(p => p.isActive);

                string? uploadedPictureUrl = null;
                if (profilePictureFile != null && profilePictureFile.Length > 0)
                {
                    uploadedPictureUrl = await SaveProfilePictureFileAsync(profilePictureFile);
                }

                if (existingEntity == null)
                {
                    var newEntity = new mstPersonal
                    {
                        name = personalDto.name,
                        profilePictureUrl = uploadedPictureUrl ?? personalDto.profilePictureUrl,
                        mobileNumber = personalDto.mobileNumber,
                        email = personalDto.email,
                        location = personalDto.location,
                        bio = personalDto.bio,
                        workDetails = personalDto.workDetails,
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstPersonals.AddAsync(newEntity);
                }
                else
                {
                    existingEntity.name = personalDto.name;
                    if (!string.IsNullOrEmpty(uploadedPictureUrl))
                    {
                        existingEntity.profilePictureUrl = uploadedPictureUrl;
                    }
                    existingEntity.mobileNumber = personalDto.mobileNumber;
                    existingEntity.email = personalDto.email;
                    existingEntity.location = personalDto.location;
                    existingEntity.bio = personalDto.bio;
                    existingEntity.workDetails = personalDto.workDetails;
                    existingEntity.updatedAt = DateTime.UtcNow;
                    _context.mstPersonals.Update(existingEntity);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Personal details saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save personal details.",
                    errorDetails = ex.Message
                });
            }
        }

        public async Task<ApiResponseDTO> SaveAboutSectionAsync(PersonalDTO personalDto)
        {
            try
            {
                var existingEntity = await _context.mstPersonals
                    .FirstOrDefaultAsync(p => p.isActive);

                if (existingEntity == null)
                {
                    var newEntity = new mstPersonal
                    {
                        professionalSummary = personalDto.professionalSummary,
                        yearsOfExperience = personalDto.yearsOfExperience,
                        projectsCompleted = personalDto.projectsCompleted,
                        companiesWorked = personalDto.companiesWorked,
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstPersonals.AddAsync(newEntity);
                }
                else
                {
                    existingEntity.professionalSummary = personalDto.professionalSummary;
                    existingEntity.yearsOfExperience = personalDto.yearsOfExperience;
                    existingEntity.projectsCompleted = personalDto.projectsCompleted;
                    existingEntity.companiesWorked = personalDto.companiesWorked;
                    existingEntity.updatedAt = DateTime.UtcNow;
                    _context.mstPersonals.Update(existingEntity);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "About section saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save about section.",
                    errorDetails = ex.Message
                });
            }
        }

        public async Task<ApiResponseDTO> SaveSocialMediaAsync(PersonalDTO personalDto)
        {
            try
            {
                var existingEntity = await _context.mstPersonals
                    .FirstOrDefaultAsync(p => p.isActive);

                if (existingEntity == null)
                {
                    var newEntity = new mstPersonal
                    {
                        linkedInUrl = personalDto.linkedInUrl,
                        gitHubUrl = personalDto.gitHubUrl,
                        gitLabUrl = personalDto.gitLabUrl,
                        orcidUrl = personalDto.orcidUrl,
                        googleScholarUrl = personalDto.googleScholarUrl,
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstPersonals.AddAsync(newEntity);
                }
                else
                {
                    existingEntity.linkedInUrl = personalDto.linkedInUrl;
                    existingEntity.gitHubUrl = personalDto.gitHubUrl;
                    existingEntity.gitLabUrl = personalDto.gitLabUrl;
                    existingEntity.orcidUrl = personalDto.orcidUrl;
                    existingEntity.googleScholarUrl = personalDto.googleScholarUrl;
                    existingEntity.updatedAt = DateTime.UtcNow;
                    _context.mstPersonals.Update(existingEntity);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Social media links saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save social media links.",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion

        #region Personal - Utility Methods
        private async Task<string> SaveProfilePictureFileAsync(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profile");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/uploads/profile/" + uniqueFileName;
        }
        #endregion
    }
}
