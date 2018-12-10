using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace AmazingCo.Api.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CompanyStructureContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CompanyStructureContext>>()))
            {
                if (!context.Companies.Any())
                {
                    var rootId = Guid.NewGuid();
                    var idb = Guid.NewGuid();
                    var companies = new List<Company>()
                    {
                        new Company {Name = "A", ParentId = null, ExternalId = rootId},
                        new Company {Name = "B", ParentId = rootId, ExternalId = idb},
                        new Company {Name = "C", ParentId = rootId, ExternalId = Guid.NewGuid()},
                        new Company {Name = "E", ParentId = idb, ExternalId = Guid.NewGuid()},
                        new Company {Name = "F", ParentId = idb, ExternalId = Guid.NewGuid()}
                    };

                    context.Companies.AddRange(companies);
                    context.SaveChanges();
                    context.Dispose();
                }
            }
        }
    }
}
