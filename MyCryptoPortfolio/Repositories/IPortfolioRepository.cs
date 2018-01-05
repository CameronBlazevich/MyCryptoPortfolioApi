using MyCryptoPortfolio.Models;

namespace MyCryptoPortfolio.Repositories
{
    public interface IPortfolioRepository
    {
        Portfolio GetPortfolio(string userId);
    }
}