using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AmazingCo.Api.Data.Cache
{
    public class CompanyStructureCache : ICompanyStructureCache
    {
        private readonly ICompanyRepository _companiesRepository;
        private static object _lockAllCompanies = new object();
        private IEnumerable<Company> _allCompanies;

        public CompanyStructureCache(ICompanyRepository companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        public IEnumerable<Company> Companies
        {
            get
            {
                lock (_lockAllCompanies)
                {
                    if (_allCompanies == null)
                    {
                        _allCompanies = _companiesRepository.GetAllNodes();
                    }
                }

                return _allCompanies;
            }
        }

        public IEnumerable<Company> GetParentsBy(Company communication, bool includeCurrrent)
        {
            throw new System.NotImplementedException();
        }
    }
}
