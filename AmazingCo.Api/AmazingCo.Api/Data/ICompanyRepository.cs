using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCo.Api.Data
{
    public interface ICompanyRepository
    {
        Task<string> GetChildrenNodes(string node);
    }
}
