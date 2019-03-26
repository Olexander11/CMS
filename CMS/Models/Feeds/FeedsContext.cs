using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class FeedsContext : DbContext
    {
        public DbSet<CollectionFeeds> Collections { get; set; }
        public DbSet<NewsItem> News { get; set; }
        public DbSet<Link> Links { get; set; }

        public FeedsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=cmsdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            base.OnModelCreating(modelBuilder);

        }
    }
}
