using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyCryptoPortfolio.Controllers
{
    public class MyCryptoPortfolioBaseController : Controller
    {
        public string UserId => GetLoggedInUser();

        public string GetLoggedInUser()
        {
            if (Debugger.IsAttached)
            {
                return "debugUser";
            }

            return "debugUser";
        }
    }
}
