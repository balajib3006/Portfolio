using BalajiPortfolio.Helpers.DbContexts;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using BalajiPortfolio.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio.Repositories
{
    public class PublicationRepository : IPublicationRepository
    {
        #region DB Context and Interfaces
        private readonly appDBContext _context;
        private readonly IApiResponseRepository _apiResponseRepository;

        public PublicationRepository(appDBContext context, IApiResponseRepository apiResponseRepository)
        {
            _context = context;
            _apiResponseRepository = apiResponseRepository;
        }
        #endregion

        #region Publication - Query Operations
        public async Task<List<PublicationDTO>> GetAllPublicationsAsync()
        {
            return await _context.mstPublications
                .Where(p => p.isActive)
                .OrderByDescending(p => p.mstPublicationId)
                .Select(p => new PublicationDTO
                {
                    mstPublicationId = p.mstPublicationId,
                    title = p.title,
                    abstractText = p.abstractText,
                    publisher = p.publisher,
                    publicationDate = p.publicationDate,
                    publicationLink = p.publicationLink,
                    isActive = p.isActive,
                    updatedAt = p.updatedAt
                })
                .ToListAsync();
        }

        public async Task<PublicationDTO?> GetPublicationByIdAsync(int id)
        {
            return await _context.mstPublications
                .Where(p => p.mstPublicationId == id && p.isActive)
                .Select(p => new PublicationDTO
                {
                    mstPublicationId = p.mstPublicationId,
                    title = p.title,
                    abstractText = p.abstractText,
                    publisher = p.publisher,
                    publicationDate = p.publicationDate,
                    publicationLink = p.publicationLink,
                    isActive = p.isActive,
                    updatedAt = p.updatedAt
                })
                .FirstOrDefaultAsync();
        }
        #endregion

        #region Publication - CRUD Operations
        public async Task<ApiResponseDTO> SavePublicationAsync(PublicationDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.title))
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 400,
                        message = "Publication title is required."
                    });
                }

                if (dto.mstPublicationId == 0)
                {
                    var newEntity = new mstPublication
                    {
                        title = dto.title.Trim(),
                        abstractText = dto.abstractText?.Trim(),
                        publisher = dto.publisher?.Trim(),
                        publicationDate = dto.publicationDate?.Trim(),
                        publicationLink = dto.publicationLink?.Trim(),
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstPublications.AddAsync(newEntity);
                }
                else
                {
                    var existing = await _context.mstPublications
                        .FirstOrDefaultAsync(p => p.mstPublicationId == dto.mstPublicationId && p.isActive);

                    if (existing == null)
                    {
                        return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                        {
                            statusCode = 404,
                            message = "Publication record not found."
                        });
                    }

                    existing.title = dto.title.Trim();
                    existing.abstractText = dto.abstractText?.Trim();
                    existing.publisher = dto.publisher?.Trim();
                    existing.publicationDate = dto.publicationDate?.Trim();
                    existing.publicationLink = dto.publicationLink?.Trim();
                    existing.updatedAt = DateTime.UtcNow;

                    _context.mstPublications.Update(existing);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Publication saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save publication record.",
                    errorDetails = ex.Message
                });
            }
        }

        public async Task<ApiResponseDTO> DeletePublicationAsync(int id)
        {
            try
            {
                var existing = await _context.mstPublications
                    .FirstOrDefaultAsync(p => p.mstPublicationId == id && p.isActive);

                if (existing == null)
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 404,
                        message = "Publication record not found."
                    });
                }

                existing.isActive = false;
                existing.updatedAt = DateTime.UtcNow;
                _context.mstPublications.Update(existing);

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Publication deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to delete publication record.",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion
    }
}
