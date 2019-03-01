using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcoresample.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoresample
{
    public class DotNetSampleDbContext : DbContext
    {
    public DotNetSampleDbContext(DbContextOptions<DotNetSampleDbContext> options)
        : base(options)
    { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DotNetSampleDbContext).Assembly); // all configurations are applied here
        }
    }
}
