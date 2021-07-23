﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroservice.DTOs
{
    public class CompanyDto
    {
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCeo { get; set; }
        public decimal Turnover { get; set; }
        public string Website { get; set; }
        public string StockExchange { get; set; }
        public int StockExchangeID { get; set; }
    }
}
