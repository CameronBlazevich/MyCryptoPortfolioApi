using Newtonsoft.Json;

namespace MyCryptoPortfolio.Models
{
    public class Coin
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("rank")]
        public int Rank { get; set; }
        [JsonProperty("price_usd")]
        public decimal? PriceUsd { get; set; }
        [JsonProperty("price_btc")]
        public float? PriceBtc { get; set; }
        [JsonProperty("24h_volume_usd")]
        public decimal? OneDayVolume { get; set; }
        [JsonProperty("market_cap_usd")]
        public decimal? MarketCapUsd { get; set; }
        [JsonProperty("available_supply")]
        public decimal? AvailableSupply { get; set; }
        [JsonProperty("total_supply")]
        public decimal? TotalSupply { get; set; }
        [JsonProperty("percent_change_1h")]
        public float? PercentChangeOneHr { get; set; }
        [JsonProperty("percent_change_24h")]
        public float? PercentChangeOneDay { get; set; }
        [JsonProperty("percent_change_7d")]
        public float? PercentChangeOneWeek { get; set; }
        [JsonProperty("last_updated")]
        public int? LastUpdated { get; set; }
    }
}
