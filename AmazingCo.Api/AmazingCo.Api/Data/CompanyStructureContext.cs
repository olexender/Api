using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AmazingCo.Api.Data
{
    public class CompanyStructureContext : DbContext
    {
        public CompanyStructureContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}
