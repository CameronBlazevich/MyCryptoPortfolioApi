using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCryptoPortfolio.Models;

namespace MyCryptoPortfolio.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly PortfolioContext _ctx;
        private readonly ITickerDataService _tickerDataService;

        public PortfolioService(PortfolioContext ctx, ITickerDataService tickerDataService)
        {
            _ctx = ctx;
            _tickerDataService = tickerDataService;
        }
        public Portfolio GetPortfolio(string userId)
        {
            var portfolio = _ctx.Portfolios.Include("Holdings").FirstOrDefault(p => p.UserId == userId);
            PopulateCoinInfoFromCoinMarketCap(portfolio);

            return portfolio;
        }

        private void PopulateCoinInfoFromCoinMarketCap(Portfolio portfolio)
        {
            //var tickerService = new TickerDataService();
            var coinsFromCoinMarketCap = _tickerDataService.GetTickerResult();

            foreach (Holding holding in portfolio.Holdings)
            {
                holding.Coin = coinsFromCoinMarketCap.Coins.FirstOrDefault(c => c.Symbol == holding.CoinTickerSymbol);
            }
        }

        public Portfolio CreatePortfolio(string userId)
        {
            var portfolio = new Portfolio
            {
                UserId = userId
            };
            _ctx.Portfolios.Add(portfolio);
            _ctx.SaveChanges();

            return portfolio;
        }
    }
}
