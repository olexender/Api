using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCo.Api.Data
{
    public interface ICompanyRepository
    {
        Company GetNode(int node);
        void UpdateStructure(IEnumerable<Company> companies);
        IEnumerable<Company> GetAllNodes();
        void UpdateNode(Company company);
    }
}
