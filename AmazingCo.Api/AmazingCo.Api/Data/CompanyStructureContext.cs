using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmazingCo.Api.Data
{
    public class CompanyStructureContext : DbContext
    {
        public CompanyStructureContext(DbContextOptions<CompanyStructureContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}
