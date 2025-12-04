using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApp.API.Entities;

namespace SampleApp.API.Data
{
    public class SampleAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SampleAppContext(DbContextOptions<SampleAppContext> opt) : base(opt)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.4.90;Port=5432;Database=04ip215;Username=04ip215;Password=04ip215");
        }
    }

}