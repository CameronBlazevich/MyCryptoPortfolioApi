using MyCryptoPortfolio.Models.Enums;

namespace MyCryptoPortfolio.Models
{
    public class Money
    {
        public decimal Value { get; set; }
        public MoneyType Type { get; set; }
    }
}
