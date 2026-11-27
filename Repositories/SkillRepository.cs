using BalajiPortfolio.Helpers.DbContexts;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using BalajiPortfolio.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        #region DB Context and Interface References
        private readonly appDBContext _context;
        private readonly IApiResponseRepository _apiResponseRepository;

        public SkillRepository(appDBContext context, IApiResponseRepository apiResponseRepository)
        {
            _context = context;
            _apiResponseRepository = apiResponseRepository;
        }
        #endregion

        #region Skill - Query Operations
        public async Task<List<SkillDTO>> GetAllSkillsAsync()
        {
            return await _context.mstSkills
                .Where(s => s.isActive)
                .OrderBy(s => s.skillName)
                .Select(s => new SkillDTO
                {
                    mstSkillId = s.mstSkillId,
                    skillName = s.skillName,
                    proficiencyLevel = s.proficiencyLevel,
                    isActive = s.isActive,
                    updatedAt = s.updatedAt
                })
                .ToListAsync();
        }

        public async Task<SkillDTO?> GetSkillByIdAsync(int id)
        {
            return await _context.mstSkills
                .Where(s => s.mstSkillId == id && s.isActive)
                .Select(s => new SkillDTO
                {
                    mstSkillId = s.mstSkillId,
                    skillName = s.skillName,
                    proficiencyLevel = s.proficiencyLevel,
                    isActive = s.isActive,
                    updatedAt = s.updatedAt
                })
                .FirstOrDefaultAsync();
        }
        #endregion

        #region Skill - CRUD Operations
        public async Task<ApiResponseDTO> SaveSkillAsync(SkillDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.skillName))
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 400,
                        message = "Skill name is required."
                    });
                }

                if (dto.mstSkillId == 0)
                {
                    var newSkill = new mstSkill
                    {
                        skillName = dto.skillName.Trim(),
                        proficiencyLevel = dto.proficiencyLevel ?? "Advanced",
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstSkills.AddAsync(newSkill);
                }
                else
                {
                    var existing = await _context.mstSkills
                        .FirstOrDefaultAsync(s => s.mstSkillId == dto.mstSkillId && s.isActive);

                    if (existing == null)
                    {
                        return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                        {
                            statusCode = 404,
                            message = "Skill record not found."
                        });
                    }

                    existing.skillName = dto.skillName.Trim();
                    existing.proficiencyLevel = dto.proficiencyLevel ?? "Advanced";
                    existing.updatedAt = DateTime.UtcNow;

                    _context.mstSkills.Update(existing);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Skill saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save skill.",
                    errorDetails = ex.Message
                });
            }
        }

        public async Task<ApiResponseDTO> DeleteSkillAsync(int id)
        {
            try
            {
                var existing = await _context.mstSkills
                    .FirstOrDefaultAsync(s => s.mstSkillId == id && s.isActive);

                if (existing == null)
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 404,
                        message = "Skill not found."
                    });
                }

                existing.isActive = false;
                existing.updatedAt = DateTime.UtcNow;
                _context.mstSkills.Update(existing);

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Skill deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to delete skill.",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion
    }
}
