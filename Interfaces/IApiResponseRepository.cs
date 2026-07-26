using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Interfaces
{
    public interface IApiResponseRepository
    {
        #region Response Formatting Operations
        ApiResponseDTO SuccessResponse(ApiResponseDTO dto);
        ApiResponseDTO FailureResponse(ApiResponseDTO dto);
        #endregion
    }
}
