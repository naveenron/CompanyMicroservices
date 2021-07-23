﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroservice.Model
{
    public partial class CompanyDetail
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCeo { get; set; }
        public decimal Turnover { get; set; }
        public string Website { get; set; }
        public int? StockExchange { get; set; }

        public virtual StockExchange StockExchangeNavigation { get; set; }
    }
}
