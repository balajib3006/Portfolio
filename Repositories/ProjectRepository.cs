using BalajiPortfolio.Helpers.DbContexts;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;
using BalajiPortfolio.Models.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BalajiPortfolio.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        #region DB Context, Interfaces and Environment
        private readonly appDBContext _context;
        private readonly IApiResponseRepository _apiResponseRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectRepository(
            appDBContext context,
            IApiResponseRepository apiResponseRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _apiResponseRepository = apiResponseRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Project - Query Operations
        public async Task<List<ProjectDTO>> GetAllProjectsAsync()
        {
            var entities = await _context.mstProjects
                .Where(p => p.isActive)
                .OrderByDescending(p => p.mstProjectId)
                .ToListAsync();

            var dtoList = new List<ProjectDTO>();
            foreach (var p in entities)
            {
                dtoList.Add(MapEntityToDto(p));
            }
            return dtoList;
        }

        public async Task<ProjectDTO?> GetProjectByIdAsync(int id)
        {
            var entity = await _context.mstProjects
                .FirstOrDefaultAsync(p => p.mstProjectId == id && p.isActive);

            if (entity == null) return null;
            return MapEntityToDto(entity);
        }
        #endregion

        #region Project - CRUD Operations
        public async Task<ApiResponseDTO> SaveProjectAsync(ProjectDTO dto, IFormFile? productImageFile, IFormFile? cadFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.projectTitle))
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 400,
                        message = "Project title is required."
                    });
                }

                // Handle product image upload
                string? uploadedImageUrl = null;
                if (productImageFile != null && productImageFile.Length > 0)
                {
                    uploadedImageUrl = await SaveProductImageAsync(productImageFile);
                }

                // Handle CAD file upload
                string? uploadedCadPath = null;
                if (cadFile != null && cadFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(cadFile.FileName).ToLowerInvariant();
                    var allowedExtensions = new[] { ".step", ".stp", ".iges" };
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                        {
                            statusCode = 400,
                            message = "Invalid CAD file format. Only .step, .stp, and .iges formats are allowed."
                        });
                    }

                    uploadedCadPath = await SaveCadFileAsync(cadFile);
                }

                // Clean tags list
                var cleanTags = dto.tagsList
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .Select(t => t.Trim())
                    .ToList();

                string jsonTags = JsonSerializer.Serialize(cleanTags);

                if (dto.mstProjectId == 0)
                {
                    var newProject = new mstProject
                    {
                        projectTitle = dto.projectTitle.Trim(),
                        clientName = dto.clientName?.Trim(),
                        startDate = dto.startDate?.Trim(),
                        endDate = dto.endDate?.Trim(),
                        description = dto.description,
                        productImageUrl = uploadedImageUrl ?? dto.productImageUrl,
                        cadFilePath = uploadedCadPath ?? dto.cadFilePath,
                        tags = jsonTags,
                        isActive = true,
                        updatedAt = DateTime.UtcNow
                    };
                    await _context.mstProjects.AddAsync(newProject);
                }
                else
                {
                    var existing = await _context.mstProjects
                        .FirstOrDefaultAsync(p => p.mstProjectId == dto.mstProjectId && p.isActive);

                    if (existing == null)
                    {
                        return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                        {
                            statusCode = 404,
                            message = "Project record not found."
                        });
                    }

                    existing.projectTitle = dto.projectTitle.Trim();
                    existing.clientName = dto.clientName?.Trim();
                    existing.startDate = dto.startDate?.Trim();
                    existing.endDate = dto.endDate?.Trim();
                    existing.description = dto.description;
                    if (!string.IsNullOrEmpty(uploadedImageUrl))
                    {
                        existing.productImageUrl = uploadedImageUrl;
                    }
                    if (!string.IsNullOrEmpty(uploadedCadPath))
                    {
                        existing.cadFilePath = uploadedCadPath;
                    }
                    existing.tags = jsonTags;
                    existing.updatedAt = DateTime.UtcNow;

                    _context.mstProjects.Update(existing);
                }

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Project record saved successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to save project record.",
                    errorDetails = ex.Message
                });
            }
        }

        public async Task<ApiResponseDTO> DeleteProjectAsync(int id)
        {
            try
            {
                var existing = await _context.mstProjects
                    .FirstOrDefaultAsync(p => p.mstProjectId == id && p.isActive);

                if (existing == null)
                {
                    return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                    {
                        statusCode = 404,
                        message = "Project record not found."
                    });
                }

                existing.isActive = false;
                existing.updatedAt = DateTime.UtcNow;
                _context.mstProjects.Update(existing);

                await _context.SaveChangesAsync();

                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO
                {
                    statusCode = 200,
                    message = "Project deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO
                {
                    statusCode = 500,
                    message = "Failed to delete project record.",
                    errorDetails = ex.Message
                });
            }
        }
        #endregion

        #region Project - File Upload Operations
        private async Task<string> SaveProductImageAsync(IFormFile file)
        {
            var webRoot = _webHostEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadDir = Path.Combine(webRoot, "uploads", "projects", "images");

            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadDir, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/uploads/projects/images/" + uniqueFileName;
        }

        private async Task<string> SaveCadFileAsync(IFormFile file)
        {
            var webRoot = _webHostEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadDir = Path.Combine(webRoot, "uploads", "projects", "cad");

            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadDir, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/uploads/projects/cad/" + uniqueFileName;
        }

        private ProjectDTO MapEntityToDto(mstProject entity)
        {
            var list = new List<string>();
            if (!string.IsNullOrEmpty(entity.tags))
            {
                try
                {
                    list = JsonSerializer.Deserialize<List<string>>(entity.tags) ?? new List<string>();
                }
                catch
                {
                    list = entity.tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim()).ToList();
                }
            }

            return new ProjectDTO
            {
                mstProjectId = entity.mstProjectId,
                projectTitle = entity.projectTitle,
                clientName = entity.clientName,
                startDate = entity.startDate,
                endDate = entity.endDate,
                description = entity.description,
                productImageUrl = entity.productImageUrl,
                cadFilePath = entity.cadFilePath,
                tags = entity.tags,
                tagsList = list,
                isActive = entity.isActive,
                updatedAt = entity.updatedAt
            };
        }
        #endregion
    }
}
