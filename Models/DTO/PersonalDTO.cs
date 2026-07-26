namespace BalajiPortfolio.Models.DTO
{
    public class PersonalDTO
    {
        public int mstPersonalId { get; set; }

        #region Personal Details
        public string? name { get; set; }
        public string? profilePictureUrl { get; set; }
        public string? mobileNumber { get; set; }
        public string? email { get; set; }
        public string? location { get; set; }
        public string? bio { get; set; }
        public string? workDetails { get; set; }
        #endregion

        #region About Section Details
        public string? professionalSummary { get; set; }
        public string? yearsOfExperience { get; set; }
        public string? projectsCompleted { get; set; }
        public string? companiesWorked { get; set; }
        #endregion

        #region Social Media URLs
        public string? linkedInUrl { get; set; }
        public string? gitHubUrl { get; set; }
        public string? gitLabUrl { get; set; }
        public string? orcidUrl { get; set; }
        public string? googleScholarUrl { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
}
