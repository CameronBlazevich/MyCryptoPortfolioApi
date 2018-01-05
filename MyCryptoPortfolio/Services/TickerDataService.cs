using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCryptoPortfolio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MyCryptoPortfolio.Services
{
    public interface ITickerDataService
    {
        TickerResult GetTickerResult();
    }

    public class TickerDataService : ITickerDataService
    {
        private DateTime LastHydrationDateTime { get; set; }
        private TickerResult TickerResult { get; set; } = new TickerResult();
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public TickerDataService(ILogger<TickerDataService> logger, IConfiguration config )
        {
            _logger = logger;
            _config = config;
        }
        public TickerResult GetTickerResult()
        {
            if (LastHydrationDateTime.AddMinutes(1) > DateTime.UtcNow && TickerResult.Coins != null)
            {
                _logger.LogInformation("Getting ticker result from cache");
                return  TickerResult;
            }

            _logger.LogInformation("Getting ticker result from third party");
            using (var client = new HttpClient())
            {
                try
                {
                    var coinMarketCapAppSettings = _config.GetSection("coinMarketCap");
                    client.BaseAddress = new Uri(coinMarketCapAppSettings["baseUrl"]);
                    var data =  client.GetAsync($"{coinMarketCapAppSettings["endpoint"]}{coinMarketCapAppSettings["resultsLimit"]}").Result;
                    var jsonResponse = data.Content.ReadAsStringAsync().Result;

                    var tickerResultList = JsonConvert.DeserializeObject<IEnumerable<Coin>>(jsonResponse) as IList<Coin> ??
                                           JsonConvert.DeserializeObject<IEnumerable<Coin>>(jsonResponse).ToList();

                    TickerResult.Coins = tickerResultList.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"GetTickerResult failed. Returning stale data. Exception: {ex}");
                    //throw new Exception("Shit broke");
                    return TickerResult;
                }
            }
            LastHydrationDateTime = DateTime.UtcNow;
            return TickerResult;
        }
    }
}
