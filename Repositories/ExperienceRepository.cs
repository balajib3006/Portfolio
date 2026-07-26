using System.Text.Json;
using BalajiPortfolio.Helpers.DbContexts;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using BalajiPortfolio.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio.Repositories
{
    public class ExperienceRepository : IExperienceRepository
    {
        #region DB Context and Interface References
        private readonly appDBContext _context;
        private readonly IApiResponseRepository _apiResponseRepository;

        public ExperienceRepository(appDBContext context, IApiResponseRepository apiResponseRepository)
        {
            _context = context;
            _apiResponseRepository = apiResponseRepository;
        }
        #endregion

        #region Experience - Query Operations
        public async Task<List<ExperienceDTO>> GetAllExperiencesAsync()
        {
            var entities = await _context.mstExperiences
                .Where(e => e.isActive)
                .OrderByDescending(e => e.mstExperienceId)
                .ToListAsync();

            var dtoList = new List<ExperienceDTO>();
            foreach (var e in entities)
            {
                var dto = MapEntityToDto(e);
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<ExperienceDTO?> GetExperienceByIdAsync(int id)
        {
            var entity = await _context.mstExperiences
                .FirstOrDefaultAsync(e => e.mstExperienceId == id && e.isActive);

            if (entity == null) return null;
            return MapEntityToDto(entity);
        }
        #endregion

        #region Experience - CRUD Operations
        public async Task<ApiResponseDTO> SaveExperienceAsync(ExperienceDTO dto)
        {
            try
            {
                // Clean responsibilities list
                var cleanList = dto.responsibilitiesList
                    .Where(r => !string.IsNullOrWhiteSpace(r))
                    .Select(r => r.Trim())
                    .ToList();

                string jsonResponsibilities = JsonSerializer.Serialize(cleanList);

                if (dto.mstExperienceId == 0)
                {
                    var newEntity = new mstExperience
                    {
                        jobTitle = dto.jobTitle,
                        company = dto.company,
                        location = dto.location,
                        startDate = dto.startDate,
                        endDate = dto.endDate,
                        isCurrentlyWorking = dto.isCurrentlyWorking,
                        responsibilities = jsonResponsibilities,
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstExperiences.AddAsync(newEntity);
                }
                else
                {
                    var existing = await _context.mstExperiences
                        .FirstOrDefaultAsync(e => e.mstExperienceId == dto.mstExperienceId && e.isActive);

                    if (existing == null)
                    {
                        return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                        {
                            statusCode = 404,
                            message = "Experience record not found."
                        });
                    }

                    existing.jobTitle = dto.jobTitle;
                    existing.company = dto.company;
                    existing.location = dto.location;
                    existing.startDate = dto.startDate;
                    existing.endDate = dto.endDate;
                    existing.isCurrentlyWorking = dto.isCurrentlyWorking;
                    existing.responsibilities = jsonResponsibilities;
                    existing.updatedAt = DateTime.UtcNow;

                    _context.mstExperiences.Update(existing);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Experience saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save experience record.",
                    errorDetails = ex.Message
                });
            }
        }

        public async Task<ApiResponseDTO> DeleteExperienceAsync(int id)
        {
            try
            {
                var existing = await _context.mstExperiences
                    .FirstOrDefaultAsync(e => e.mstExperienceId == id && e.isActive);

                if (existing == null)
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 404,
                        message = "Experience record not found."
                    });
                }

                // Soft Delete
                existing.isActive = false;
                existing.updatedAt = DateTime.UtcNow;
                _context.mstExperiences.Update(existing);

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Experience deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to delete experience record.",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion

        #region Experience - Utility Methods
        private ExperienceDTO MapEntityToDto(mstExperience entity)
        {
            var list = new List<string>();
            if (!string.IsNullOrEmpty(entity.responsibilities))
            {
                try
                {
                    list = JsonSerializer.Deserialize<List<string>>(entity.responsibilities) ?? new List<string>();
                }
                catch
                {
                    list = entity.responsibilities.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim()).ToList();
                }
            }

            return new ExperienceDTO
            {
                mstExperienceId = entity.mstExperienceId,
                jobTitle = entity.jobTitle,
                company = entity.company,
                location = entity.location,
                startDate = entity.startDate,
                endDate = entity.endDate,
                isCurrentlyWorking = entity.isCurrentlyWorking,
                responsibilities = entity.responsibilities,
                responsibilitiesList = list,
                isActive = entity.isActive,
                updatedAt = entity.updatedAt
            };
        }
        #endregion
    }
}
