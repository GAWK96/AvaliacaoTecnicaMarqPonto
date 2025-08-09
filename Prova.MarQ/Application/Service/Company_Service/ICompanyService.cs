using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Service.Company_Service
{
    public interface ICompanyService
    {
        Task<Company?> FilterCompanyById(int id);
        Task AddCompany(Company company);
        Task UpdateCompany(Company company);
        Task DeleteCompany(bool softDelete, int id);
        //void CheckCompanyNull(Company company);
    }
}
