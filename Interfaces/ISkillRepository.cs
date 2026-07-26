using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Interfaces
{
    public interface ISkillRepository
    {
        #region Skill & Category - Query Operations
        Task<SkillsPageDTO> GetSkillsPageDataAsync();
        Task<List<SkillCategoryDTO>> GetAllCategoriesAsync();
        Task<List<SkillDTO>> GetAllSkillsAsync();
        #endregion

        #region Skill Category - CRUD Operations
        Task<ApiResponseDTO> SaveCategoryAsync(SkillCategoryDTO dto);
        Task<ApiResponseDTO> DeleteCategoryAsync(int id);
        #endregion

        #region Skill - CRUD Operations
        Task<ApiResponseDTO> SaveSkillAsync(SkillDTO dto);
        Task<ApiResponseDTO> DeleteSkillAsync(int id);
        #endregion
    }
}
