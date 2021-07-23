using CompanyMicroservice.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroservice.Repository
{
    public interface ICompanyRepositories
    {
        List<CompanyDto> GetAll();

        List<StockExchangeDto> GetAllStock();

        CompanyDto GetCompanyDetailsByCode(string code);

        CompanyDto CreateCompany(CompanyDto companyDetails);

        CompanyDto UpdateCompany(CompanyDto companyDetails);

        bool DeleteCompany(string code);
    }
}
