using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Models.DTO
{
    public class PortfolioViewModel
    {
        public PersonalDTO Personal { get; set; } = new PersonalDTO();
        public List<ExperienceDTO> Experiences { get; set; } = new List<ExperienceDTO>();
        public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
        public List<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
        public List<CertificationDTO> Certifications { get; set; } = new List<CertificationDTO>();
        public List<PublicationDTO> Publications { get; set; } = new List<PublicationDTO>();
    }
}
