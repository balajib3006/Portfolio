using System.ComponentModel.DataAnnotations;

namespace BalajiPortfolio.Models.Entity
{
    public class mstPersonal
    {
        [Key]
        public int Id { get; set; }

        // Personal Details
        public string? Name { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public string? Bio { get; set; }
        public string? WorkDetails { get; set; }

        // About Section Details
        public string? ProfessionalSummary { get; set; }
        public string? YearsOfExperience { get; set; }
        public string? ProjectsCompleted { get; set; }
        public string? CompaniesWorked { get; set; }

        // Social Media URLs
        public string? LinkedInUrl { get; set; }
        public string? GitHubUrl { get; set; }
        public string? GitLabUrl { get; set; }
        public string? OrcidUrl { get; set; }
        public string? GoogleScholarUrl { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
