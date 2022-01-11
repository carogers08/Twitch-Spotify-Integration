using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSI_App.Database;

namespace TSI_App.Data
{
    public class TSI_DbContext : DbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Streams> Streams { get; set; }
        public DbSet<Songs> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(local);Initial Catalog=TSI;User ID =sa;Password=1Corinthians1:30");
        }
    }
}
