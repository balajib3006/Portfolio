using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Interfaces
{
    public interface IExperienceRepository
    {
        #region Experience - Query Operations
        Task<List<ExperienceDTO>> GetAllExperiencesAsync();
        Task<ExperienceDTO?> GetExperienceByIdAsync(int id);
        #endregion

        #region Experience - CRUD Operations
        Task<ApiResponseDTO> SaveExperienceAsync(ExperienceDTO dto);
        Task<ApiResponseDTO> DeleteExperienceAsync(int id);
        #endregion
    }
}
