using System.Collections.Generic;
using MyCryptoPortfolio.Models;

namespace MyCryptoPortfolio.Repositories
{
    public interface IHoldingsRepository
    {
        Holding GetHolding(string userId, string tickerSymbol);
        Holding CreateHolding(Holding holding);
        Holding UpdateHolding(Holding holding);
        void DeleteHolding(Holding holding);
        IEnumerable<Holding> GetHoldings(string userId);
    }
}
