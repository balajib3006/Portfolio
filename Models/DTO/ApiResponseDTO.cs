namespace BalajiPortfolio.Models.DTO
{
    public class ApiResponseDTO
    {
        public int statusCode { get; set; } = 200;
        public bool success { get; set; } = true;
        public string message { get; set; } = string.Empty;
        public object? data { get; set; }
        public object? errorDetails { get; set; }
    }
}
