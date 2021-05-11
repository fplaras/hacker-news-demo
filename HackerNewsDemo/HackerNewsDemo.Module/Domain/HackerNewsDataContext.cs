using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsDemo.Module.Domain
{
    public class HackerNewsDataContext : DbContext
    {
        public HackerNewsDataContext(DbContextOptions<HackerNewsDataContext> options)
        : base(options)
        { }
        public DbSet<HackerNewsItemDomain> HackerNewsItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "Demo");
            }
        }
    }
}
