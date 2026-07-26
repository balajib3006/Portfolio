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

        #region Skill & Category - Query Operations
        public async Task<SkillsPageDTO> GetSkillsPageDataAsync()
        {
            var categories = await GetAllCategoriesAsync();
            var skills = await GetAllSkillsAsync();

            return new SkillsPageDTO
            {
                categories = categories,
                skills = skills
            };
        }

        public async Task<List<SkillCategoryDTO>> GetAllCategoriesAsync()
        {
            return await _context.mstSkillCategories
                .Where(c => c.isActive)
                .OrderBy(c => c.categoryName)
                .Select(c => new SkillCategoryDTO
                {
                    mstSkillCategoryId = c.mstSkillCategoryId,
                    categoryName = c.categoryName,
                    isActive = c.isActive,
                    updatedAt = c.updatedAt
                })
                .ToListAsync();
        }

        public async Task<List<SkillDTO>> GetAllSkillsAsync()
        {
            return await _context.mstSkills
                .Where(s => s.isActive)
                .Join(_context.mstSkillCategories.Where(c => c.isActive),
                    s => s.mstSkillCategoryId,
                    c => c.mstSkillCategoryId,
                    (s, c) => new SkillDTO
                    {
                        mstSkillId = s.mstSkillId,
                        mstSkillCategoryId = s.mstSkillCategoryId,
                        categoryName = c.categoryName,
                        skillName = s.skillName,
                        proficiencyLevel = s.proficiencyLevel,
                        isActive = s.isActive,
                        updatedAt = s.updatedAt
                    })
                .OrderBy(s => s.categoryName)
                .ThenBy(s => s.skillName)
                .ToListAsync();
        }
        #endregion

        #region Skill Category - CRUD Operations
        public async Task<ApiResponseDTO> SaveCategoryAsync(SkillCategoryDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.categoryName))
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 400,
                        message = "Category name is required."
                    });
                }

                if (dto.mstSkillCategoryId == 0)
                {
                    var newCategory = new mstSkillCategory
                    {
                        categoryName = dto.categoryName.Trim(),
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstSkillCategories.AddAsync(newCategory);
                }
                else
                {
                    var existing = await _context.mstSkillCategories
                        .FirstOrDefaultAsync(c => c.mstSkillCategoryId == dto.mstSkillCategoryId && c.isActive);

                    if (existing == null)
                    {
                        return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                        {
                            statusCode = 404,
                            message = "Skill category not found."
                        });
                    }

                    existing.categoryName = dto.categoryName.Trim();
                    existing.updatedAt = DateTime.UtcNow;
                    _context.mstSkillCategories.Update(existing);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Skill category saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save skill category.",
                    errorDetails = ex.Message
                });
            }
        }

        public async Task<ApiResponseDTO> DeleteCategoryAsync(int id)
        {
            try
            {
                var existing = await _context.mstSkillCategories
                    .FirstOrDefaultAsync(c => c.mstSkillCategoryId == id && c.isActive);

                if (existing == null)
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 404,
                        message = "Skill category not found."
                    });
                }

                // Soft Delete Category
                existing.isActive = false;
                existing.updatedAt = DateTime.UtcNow;
                _context.mstSkillCategories.Update(existing);

                // Soft Delete associated skills
                var associatedSkills = await _context.mstSkills
                    .Where(s => s.mstSkillCategoryId == id && s.isActive)
                    .ToListAsync();

                foreach (var s in associatedSkills)
                {
                    s.isActive = false;
                    s.updatedAt = DateTime.UtcNow;
                    _context.mstSkills.Update(s);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Skill category deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to delete skill category.",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion

        #region Skill - CRUD Operations
        public async Task<ApiResponseDTO> SaveSkillAsync(SkillDTO dto)
        {
            try
            {
                if (dto.mstSkillCategoryId == 0)
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 400,
                        message = "Please select a valid skill category."
                    });
                }

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
                        mstSkillCategoryId = dto.mstSkillCategoryId,
                        skillName = dto.skillName.Trim(),
                        proficiencyLevel = dto.proficiencyLevel,
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

                    existing.mstSkillCategoryId = dto.mstSkillCategoryId;
                    existing.skillName = dto.skillName.Trim();
                    existing.proficiencyLevel = dto.proficiencyLevel;
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
                    message = "Failed to save skill record.",
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
                        message = "Skill record not found."
                    });
                }

                // Soft Delete
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
                    message = "Failed to delete skill record.",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion
    }
}
