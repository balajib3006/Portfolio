using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Interfaces
{
    public interface ICertificationRepository
    {
        #region Certification - Query Operations
        Task<List<CertificationDTO>> GetAllCertificationsAsync();
        Task<CertificationDTO?> GetCertificationByIdAsync(int id);
        #endregion

        #region Certification - CRUD Operations
        Task<ApiResponseDTO> SaveCertificationAsync(CertificationDTO dto);
        Task<ApiResponseDTO> DeleteCertificationAsync(int id);
        #endregion
    }
}
