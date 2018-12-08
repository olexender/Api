using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCo.Api.Data
{
    public interface ICompanyRepository
    {
        Company GetChildrenNodes(string node);
        IEnumerable<Company> GetAllNodes();
    }
}
