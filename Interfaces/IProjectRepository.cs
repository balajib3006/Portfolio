using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Http;

namespace BalajiPortfolio.Interfaces
{
    public interface IProjectRepository
    {
        #region Project - Query Operations
        Task<List<ProjectDTO>> GetAllProjectsAsync();
        Task<ProjectDTO?> GetProjectByIdAsync(int id);
        #endregion

        #region Project - CRUD Operations
        Task<ApiResponseDTO> SaveProjectAsync(ProjectDTO dto, IFormFile? productImageFile, IFormFile? cadFile);
        Task<ApiResponseDTO> DeleteProjectAsync(int id);
        #endregion
    }
}
