using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyCryptoPortfolio.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IList<Holding> Holdings { get; set; } = new List<Holding>();

        [NotMapped]
        public decimal TotalValue
        {
            get { return Holdings.Sum(h => h.ValueOfAmountOwned.Value); }
        }
    }
}
