using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Interfaces
{
    public interface ISkillRepository
    {
        #region Skill - Query Operations
        Task<List<SkillDTO>> GetAllSkillsAsync();
        Task<SkillDTO?> GetSkillByIdAsync(int id);
        #endregion

        #region Skill - CRUD Operations
        Task<ApiResponseDTO> SaveSkillAsync(SkillDTO dto);
        Task<ApiResponseDTO> DeleteSkillAsync(int id);
        #endregion
    }
}
