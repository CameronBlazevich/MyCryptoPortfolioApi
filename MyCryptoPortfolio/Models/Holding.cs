using System.ComponentModel.DataAnnotations.Schema;
using MyCryptoPortfolio.Models.Enums;

namespace MyCryptoPortfolio.Models
{
    public class Holding
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string CoinTickerSymbol { get; set; }
        public float AmountOwned { get; set; }
        public string UserId { get; set; }


        [NotMapped]
        public Coin Coin { get; set; } = new Coin();

        [NotMapped]
        public Money ValueOfAmountOwned
        {
            get
            {
                var valueOfAmountOwned = new Money
                {
                    Type = MoneyType.USD,
                    Value = (decimal)((float)(Coin?.PriceUsd ?? 0) * AmountOwned)
                };

                return valueOfAmountOwned;
            }
        }

        public void FetchCoinData()
        {
            //go get coin data
        }
    }
}
