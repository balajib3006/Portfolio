using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Repositories
{
    public class WebsiteRepository : IWebsiteRepository
    {
        #region Dependency Injection
        private readonly IPersonalRepository _personalRepo;
        private readonly IExperienceRepository _experienceRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly ICertificationRepository _certificationRepo;
        private readonly IPublicationRepository _publicationRepo;

        public WebsiteRepository(
            IPersonalRepository personalRepo,
            IExperienceRepository experienceRepo,
            ISkillRepository skillRepo,
            IProjectRepository projectRepo,
            ICertificationRepository certificationRepo,
            IPublicationRepository publicationRepo)
        {
            _personalRepo = personalRepo;
            _experienceRepo = experienceRepo;
            _skillRepo = skillRepo;
            _projectRepo = projectRepo;
            _certificationRepo = certificationRepo;
            _publicationRepo = publicationRepo;
        }
        #endregion

        #region Portfolio - Query Operations
        public async Task<PortfolioViewModel> GetPortfolioDataAsync()
        {
            // DbContext is not thread-safe; execute sequentially within the scoped request
            var personal = await _personalRepo.GetPersonalDetailsAsync();
            var experiences = await _experienceRepo.GetAllExperiencesAsync();
            var skills = await _skillRepo.GetAllSkillsAsync();
            var projects = await _projectRepo.GetAllProjectsAsync();
            var certifications = await _certificationRepo.GetAllCertificationsAsync();
            var publications = await _publicationRepo.GetAllPublicationsAsync();

            return new PortfolioViewModel
            {
                Personal = personal ?? new PersonalDTO(),
                Experiences = experiences ?? new List<ExperienceDTO>(),
                Skills = skills ?? new List<SkillDTO>(),
                Projects = projects ?? new List<ProjectDTO>(),
                Certifications = certifications ?? new List<CertificationDTO>(),
                Publications = publications ?? new List<PublicationDTO>()
            };
        }
        #endregion
    }
}
