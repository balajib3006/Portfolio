namespace BalajiPortfolio.Models.DTO
{
    public class DashboardDTO
    {
        public int TotalProjects { get; set; }
        public int TotalSkills { get; set; }
        public int TotalCertifications { get; set; }
        public int TotalPublications { get; set; }
        public int TotalExperiences { get; set; }
        public string? ProfileName { get; set; }
        public bool HasProfile { get; set; }
    }
}

