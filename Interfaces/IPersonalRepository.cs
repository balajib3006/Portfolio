using BalajiPortfolio.Models.Entity;

namespace BalajiPortfolio.Interfaces
{
    public interface IPersonalRepository
    {
        Task<mstPersonal?> GetPersonalAsync();
        Task SavePersonalDetailsAsync(mstPersonal model);
        Task SaveAboutSectionAsync(mstPersonal model);
        Task SaveSocialMediaAsync(mstPersonal model);
    }
}
