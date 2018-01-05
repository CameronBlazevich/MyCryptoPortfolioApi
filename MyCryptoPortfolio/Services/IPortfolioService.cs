using MyCryptoPortfolio.Models;

namespace MyCryptoPortfolio.Services
{
    public interface IPortfolioService
    {
        Portfolio GetPortfolio(string userId);
        Portfolio CreatePortfolio(string userId);
    }
}