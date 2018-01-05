using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyCryptoPortfolio.Models;
using MyCryptoPortfolio.Models.Dtos;
using MyCryptoPortfolio.Repositories;

namespace MyCryptoPortfolio.Controllers
{
    [Route("api/[Controller]")]
    public class HoldingsController : MyCryptoPortfolioBaseController
    {
        private readonly IHoldingsRepository _holdingsRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public HoldingsController(IHoldingsRepository holdingsRepo, IPortfolioRepository portfolioRepo)
        {
            _holdingsRepo = holdingsRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        public IActionResult GetHoldings()
        {
            //DOESNT POPULATE COIN DATA YET
            var holdings = _holdingsRepo.GetHoldings(UserId);
            return Ok(holdings);
        }

        [HttpPost]
        public IActionResult AddHolding([FromBody]HoldingDto holdingDto)
        {
            if (holdingDto?.CoinTickerSymbol == null)
            {
                return BadRequest("Incomplete information");
            }

            var userPortfolio = _portfolioRepo.GetPortfolio(UserId);
            if (userPortfolio == null)
            {
                return NotFound("Porfolio for this user was not found");
            }

            if (userPortfolio.Holdings?.Any(h => h.CoinTickerSymbol == holdingDto.CoinTickerSymbol) ?? false)
            {
                return BadRequest($"Portfolio already has an entry with ticker symbol {holdingDto.CoinTickerSymbol}");
            }

            var holding = new Holding
            {
                UserId = UserId,
                AmountOwned = holdingDto.AmountOwned,
                CoinTickerSymbol = holdingDto.CoinTickerSymbol,
                PortfolioId = userPortfolio.Id
            };

            holding = _holdingsRepo.CreateHolding(holding);
            return Ok(holding);
        }

        [HttpPut]
        public IActionResult UpdateHolding([FromBody]HoldingDto holdingDto)
        {
            var holding = new Holding
            {
                CoinTickerSymbol = holdingDto.CoinTickerSymbol,
                AmountOwned = holdingDto.AmountOwned,
                UserId = UserId
            };

            holding = _holdingsRepo.UpdateHolding(holding);

            return Ok(holding);
        }

        [HttpDelete]
        public IActionResult DeleteHolding([FromBody] HoldingDto holdingDto)
        {
            var holding = new Holding
            {
                UserId = UserId,
                CoinTickerSymbol = holdingDto.CoinTickerSymbol
            };

            _holdingsRepo.DeleteHolding(holding);
            return Ok($"{holdingDto.CoinTickerSymbol} removed from portfolio");
        }
    }
}
