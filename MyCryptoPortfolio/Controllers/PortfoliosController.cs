using Microsoft.AspNetCore.Mvc;
using MyCryptoPortfolio.Services;

namespace MyCryptoPortfolio.Controllers
{

    [Route("api/[controller]")]
    public class PortfoliosController : MyCryptoPortfolioBaseController
    {
        private readonly IPortfolioService _portfolioService;

        public PortfoliosController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet]
        public IActionResult GetPortfolio()
        {

            var portfolio = _portfolioService.GetPortfolio(UserId);
            return Ok(portfolio);
        }

        [HttpPost]
        public IActionResult CreatePortfolio()
        {
            var portfolio = _portfolioService.CreatePortfolio(UserId);
            return Ok(portfolio);
        }


    }
}
