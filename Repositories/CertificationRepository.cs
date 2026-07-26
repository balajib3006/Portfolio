using BalajiPortfolio.Helpers.DbContexts;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using BalajiPortfolio.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio.Repositories
{
    public class CertificationRepository : ICertificationRepository
    {
        #region DB Context and Interface References
        private readonly appDBContext _context;
        private readonly IApiResponseRepository _apiResponseRepository;

        public CertificationRepository(appDBContext context, IApiResponseRepository apiResponseRepository)
        {
            _context = context;
            _apiResponseRepository = apiResponseRepository;
        }
        #endregion

        #region Certification - Query Operations
        public async Task<List<CertificationDTO>> GetAllCertificationsAsync()
        {
            return await _context.mstCertifications
                .Where(c => c.isActive)
                .OrderByDescending(c => c.mstCertificationId)
                .Select(c => new CertificationDTO
                {
                    mstCertificationId = c.mstCertificationId,
                    type = c.type,
                    name = c.name,
                    issuer = c.issuer,
                    issueDate = c.issueDate,
                    credentialLink = c.credentialLink,
                    embedCode = c.embedCode,
                    isActive = c.isActive,
                    updatedAt = c.updatedAt
                })
                .ToListAsync();
        }

        public async Task<CertificationDTO?> GetCertificationByIdAsync(int id)
        {
            return await _context.mstCertifications
                .Where(c => c.mstCertificationId == id && c.isActive)
                .Select(c => new CertificationDTO
                {
                    mstCertificationId = c.mstCertificationId,
                    type = c.type,
                    name = c.name,
                    issuer = c.issuer,
                    issueDate = c.issueDate,
                    credentialLink = c.credentialLink,
                    embedCode = c.embedCode,
                    isActive = c.isActive,
                    updatedAt = c.updatedAt
                })
                .FirstOrDefaultAsync();
        }
        #endregion

        #region Certification - CRUD Operations
        public async Task<ApiResponseDTO> SaveCertificationAsync(CertificationDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.name))
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 400,
                        message = "Certification / Badge name is required."
                    });
                }

                if (dto.mstCertificationId == 0)
                {
                    var newEntity = new mstCertification
                    {
                        type = string.IsNullOrWhiteSpace(dto.type) ? "Certification" : dto.type.Trim(),
                        name = dto.name.Trim(),
                        issuer = dto.issuer?.Trim(),
                        issueDate = dto.issueDate?.Trim(),
                        credentialLink = dto.credentialLink?.Trim(),
                        embedCode = dto.embedCode,
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstCertifications.AddAsync(newEntity);
                }
                else
                {
                    var existing = await _context.mstCertifications
                        .FirstOrDefaultAsync(c => c.mstCertificationId == dto.mstCertificationId && c.isActive);

                    if (existing == null)
                    {
                        return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                        {
                            statusCode = 404,
                            message = "Certification record not found."
                        });
                    }

                    existing.type = string.IsNullOrWhiteSpace(dto.type) ? "Certification" : dto.type.Trim();
                    existing.name = dto.name.Trim();
                    existing.issuer = dto.issuer?.Trim();
                    existing.issueDate = dto.issueDate?.Trim();
                    existing.credentialLink = dto.credentialLink?.Trim();
                    existing.embedCode = dto.embedCode;
                    existing.updatedAt = DateTime.UtcNow;

                    _context.mstCertifications.Update(existing);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Certification / Badge saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save certification record.",
                    errorDetails = ex.Message
                });
            }
        }

        public async Task<ApiResponseDTO> DeleteCertificationAsync(int id)
        {
            try
            {
                var existing = await _context.mstCertifications
                    .FirstOrDefaultAsync(c => c.mstCertificationId == id && c.isActive);

                if (existing == null)
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 404,
                        message = "Certification record not found."
                    });
                }

                // Soft Delete
                existing.isActive = false;
                existing.updatedAt = DateTime.UtcNow;
                _context.mstCertifications.Update(existing);

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Certification / Badge deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to delete certification record.",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion
    }
}
