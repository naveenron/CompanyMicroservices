using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroservice.Model
{
    public partial class StockExchange
    {
        public StockExchange()
        {
            CompanyDetails = new HashSet<CompanyDetail>();
        }
        public int ExchangeId { get; set; }
        public string ExchangeName { get; set; }
        public virtual ICollection<CompanyDetail> CompanyDetails { get; set; }
    }
}
