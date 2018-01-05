using Microsoft.EntityFrameworkCore;
using MyCryptoPortfolio.Models;
using System.Linq;

namespace MyCryptoPortfolio.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PortfolioContext _ctx;
        public PortfolioRepository(PortfolioContext ctx)
        {
            _ctx = ctx;
        }

        public Portfolio GetPortfolio(string userId)
        {
            return _ctx.Portfolios.Include("Holdings").FirstOrDefault(p => p.UserId == userId);
        }
    }
}
