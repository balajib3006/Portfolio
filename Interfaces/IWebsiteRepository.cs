using BalajiPortfolio.Models.DTO;

namespace BalajiPortfolio.Interfaces
{
    public interface IWebsiteRepository
    {
        #region Portfolio - Query Operations
        Task<PortfolioViewModel> GetPortfolioDataAsync();
        #endregion
    }
}
