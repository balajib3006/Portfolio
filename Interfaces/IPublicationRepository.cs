using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Interfaces
{
    public interface IPublicationRepository
    {
        #region Publication - Query Operations
        Task<List<PublicationDTO>> GetAllPublicationsAsync();
        Task<PublicationDTO?> GetPublicationByIdAsync(int id);
        #endregion

        #region Publication - CRUD Operations
        Task<ApiResponseDTO> SavePublicationAsync(PublicationDTO dto);
        Task<ApiResponseDTO> DeletePublicationAsync(int id);
        #endregion
    }
}
