using CompanyMicroservice.DBContexts;
using CompanyMicroservice.DTOs;
using CompanyMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroservice.Repository
{
    public class CompanyRepostories : ICompanyRepositories
    {
        private StockMarketContext stockMarketContext;

        public CompanyRepostories(StockMarketContext stockMarketContext)
        {
            this.stockMarketContext = stockMarketContext;
        }

        public List<CompanyDto> GetAll()
        {
            if (stockMarketContext != null)
            {
                var result = (from c in stockMarketContext.CompanyDetails
                              orderby c.CompanyName
                              select new CompanyDto
                              {
                                  CompanyCode = c.CompanyCode,
                                  CompanyName = c.CompanyName,
                                  CompanyCeo = c.CompanyCeo,
                                  Turnover = c.Turnover,
                                  Website = c.Website,
                                  StockExchange = c.StockExchangeNavigation.ExchangeName
                              }).ToList();

                return result;
            }

            return null;
        }

        public List<StockExchangeDto> GetAllStock()
        {
            if (stockMarketContext != null)
            {
                var result = (from c in stockMarketContext.StockExchanges
                              orderby c.ExchangeName
                              select new StockExchangeDto
                              {
                                  StockID = c.ExchangeId,
                                  StockName = c.ExchangeName
                              }).ToList();

                return result;
            }

            return null;
        }

        public CompanyDto GetCompanyDetailsByCode(string code)
        {
            if (stockMarketContext != null)
            {
                var result = (from c in stockMarketContext.CompanyDetails
                              where c.CompanyCode == code
                              orderby c.CompanyName
                              select new CompanyDto
                              {
                                  CompanyCode = c.CompanyCode,
                                  CompanyName = c.CompanyName,
                                  CompanyCeo = c.CompanyCeo,
                                  Turnover = c.Turnover,
                                  Website = c.Website,
                                  StockExchange = c.StockExchangeNavigation.ExchangeName
                              }).FirstOrDefault();

                return result;
            }

            return null;
        }

        public CompanyDto CreateCompany(CompanyDto CompanyDto)
        {
            if (CompanyDto != null)
            {
                var result = GetCompanyLastID(CompanyDto.CompanyCode);
                if (result != -1)
                {
                    var companyDetails = new CompanyDetail()
                    {
                        CompanyId = result + 1,
                        CompanyCode = CompanyDto.CompanyCode,
                        CompanyName = CompanyDto.CompanyName,
                        CompanyCeo = CompanyDto.CompanyCeo,
                        Turnover = CompanyDto.Turnover,
                        Website = CompanyDto.Website,
                        StockExchange = CheckStockList(CompanyDto.StockExchange)
                    };

                    stockMarketContext.Add(companyDetails);
                    stockMarketContext.SaveChanges();

                    return CompanyDto;
                }

                return null;
            }

            return null;
        }

        public CompanyDto UpdateCompany(CompanyDto CompanyDto)
        {
            if (CompanyDto != null)
            {
                var result = stockMarketContext.CompanyDetails.Where(x => x.CompanyCode == CompanyDto.CompanyCode).FirstOrDefault();
                if (result != null)
                {
                    result.CompanyName = CompanyDto.CompanyName;
                    result.CompanyCeo = CompanyDto.CompanyCeo;
                    result.Turnover = CompanyDto.Turnover;
                    result.Website = CompanyDto.Website;
                    result.StockExchange = CheckStockList(CompanyDto.StockExchange);

                    stockMarketContext.Update<CompanyDetail>(result);
                    stockMarketContext.SaveChanges();
                    return CompanyDto;
                }
            }

            return null;
        }

        public bool DeleteCompany(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var result = (from c in stockMarketContext.CompanyDetails
                              where c.CompanyCode == code
                              orderby c.CompanyName
                              select c).FirstOrDefault();

                if (result != null)
                {
                    stockMarketContext.Remove(result);
                    stockMarketContext.SaveChanges();
                    return true;
                }

                return false;
            }

            return false;
        }

        private int GetCompanyLastID(string code)
        {
            var companyList = stockMarketContext.CompanyDetails.OrderByDescending(x => x.CompanyId).ToList();

            var existingData = companyList.Where(x => x.CompanyCode == code).FirstOrDefault();
            if (existingData != null)
            {
                return -1;
            }

            return companyList.Select(x => x.CompanyId).FirstOrDefault();
        }

        private int CheckStockList(string code)
        {
            var stockList = stockMarketContext.StockExchanges.OrderByDescending(x => x.ExchangeId).ToList();

            var existingData = stockList.Where(x => x.ExchangeName == code).FirstOrDefault();
            var stockId = stockList.Select(x => x.ExchangeId).FirstOrDefault();

            if (existingData == null)
            {
                var stockDetails = new StockExchange
                {
                    ExchangeId = stockId + 1,
                    ExchangeName = code
                };

                stockMarketContext.Add(stockDetails);
                stockMarketContext.SaveChanges();

                return stockDetails.ExchangeId;
            }

            return stockId;
        }
    }
}
