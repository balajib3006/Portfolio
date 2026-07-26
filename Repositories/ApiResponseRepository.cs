using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Repositories
{
    public class ApiResponseRepository : IApiResponseRepository
    {
        #region Response Formatting Operations
        public ApiResponseDTO SuccessResponse(ApiResponseDTO dto)
        {
            dto.success = true;
            if (dto.statusCode == 0)
            {
                dto.statusCode = 200;
            }
            if (string.IsNullOrEmpty(dto.message))
            {
                dto.message = "Operation completed successfully.";
            }
            return dto;
        }

        public ApiResponseDTO FailureResponse(ApiResponseDTO dto)
        {
            dto.success = false;
            if (dto.statusCode == 0 || dto.statusCode == 200)
            {
                dto.statusCode = 400;
            }
            if (string.IsNullOrEmpty(dto.message))
            {
                dto.message = "An error occurred while processing the request.";
            }
            return dto;
        }
        #endregion
    }
}
