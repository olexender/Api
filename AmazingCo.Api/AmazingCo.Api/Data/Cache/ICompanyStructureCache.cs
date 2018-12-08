using System.Collections.Generic;

namespace AmazingCo.Api.Data.Cache
{
    public interface ICompanyStructureCache
    {
        IEnumerable<Company> Companies { get; }
        IEnumerable<Company> GetParentsBy(Company communication, bool includeCurrrent);
    }
}
