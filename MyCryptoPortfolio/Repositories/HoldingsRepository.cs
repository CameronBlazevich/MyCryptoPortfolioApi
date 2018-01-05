using Microsoft.Extensions.Logging;
using MyCryptoPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCryptoPortfolio.Repositories
{
    public class HoldingsRepository : IHoldingsRepository
    {
        private readonly PortfolioContext _ctx;
        private readonly ILogger _logger;

        public HoldingsRepository(PortfolioContext ctx, ILogger<HoldingsRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public Holding GetHolding(string userId, string tickerSymbol)
        {
            var holding = GetHolding(new Holding{UserId = userId, CoinTickerSymbol = tickerSymbol});
            return holding;
        }


        public IEnumerable<Holding> GetHoldings(string userId)
        {
            try
            {
                var holdings = _ctx.Holdings.Where(h => h.UserId == userId).ToList();
                return holdings;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving holdings for user {userId}", ex);
                throw;
            }
           
        }

        public Holding CreateHolding(Holding holding)
        {
            try
            {
                _ctx.Add(holding);
                _ctx.SaveChanges();
                return holding;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating holding for user {holding?.UserId}", ex);
                throw;
            }
        }

        public Holding UpdateHolding(Holding holding)
        {
            var existingHolding = GetHolding(holding);

            existingHolding.AmountOwned = holding.AmountOwned;

            try
            {
                _ctx.Holdings.Update(existingHolding);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating holding for user {holding.UserId}", ex);
                throw;
            }

            return existingHolding;
        }

        private Holding GetHolding(Holding holding)
        {

            var dbHolding = _ctx.Holdings.FirstOrDefault(h =>
                h.UserId == holding.UserId && h.CoinTickerSymbol == holding.CoinTickerSymbol);
            if (dbHolding != null)
            {
                return dbHolding;
            }
            throw new ArgumentException($"Could not find holding with ticker symbol {holding?.CoinTickerSymbol}");
        }

        public void DeleteHolding(Holding holding)
        {
            var existingHolding = GetHolding(holding);

            try
            {
                _ctx.Holdings.Remove(existingHolding);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting holding for user {holding?.UserId}", ex);
                throw;
            }
        }

    }
}
