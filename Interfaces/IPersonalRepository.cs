using BalajiPortfolio.Models.DTO;
using Microsoft.AspNetCore.Http;

namespace BalajiPortfolio.Interfaces
{
    public interface IPersonalRepository
    {
        #region Personal - Query Operations
        Task<PersonalDTO?> GetPersonalDetailsAsync();
        #endregion

        #region Personal - CRUD Operations
        Task<ApiResponseDTO> SavePersonalDetailsAsync(PersonalDTO personalDto, IFormFile? profilePictureFile);
        Task<ApiResponseDTO> SaveAboutSectionAsync(PersonalDTO personalDto);
        Task<ApiResponseDTO> SaveSocialMediaAsync(PersonalDTO personalDto);
        #endregion
    }
}
