namespace BalajiPortfolio.Models.DTO
{
    public class ExperienceDTO
    {
        public int mstExperienceId { get; set; }

        #region Experience Details
        public string? jobTitle { get; set; }
        public string? company { get; set; }
        public string? location { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public bool isCurrentlyWorking { get; set; }
        public List<string> responsibilitiesList { get; set; } = new List<string>();
        public string? responsibilities { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
}
