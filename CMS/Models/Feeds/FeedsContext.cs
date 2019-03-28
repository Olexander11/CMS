using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class FeedsContext : DbContext
    {
        public DbSet<Collection> Collections { get; set; }
        public DbSet<NewsItem> News { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<CollectionFeed> CollectionFeed { get; set; }


        public FeedsContext()
        {
            Database.EnsureCreated();
        }

        public FeedsContext(DbContextOptions<FeedsContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CollectionFeed>().HasKey(cf => new { cf.CollectionId, cf.FeedId });

            modelBuilder.Entity<CollectionFeed>()
            .HasOne<Feed>(cf => cf.Feed)
            .WithMany(c => c.CollectionFeed)
            .HasForeignKey(cf => cf.FeedId);

            modelBuilder.Entity<CollectionFeed>()
            .HasOne<Collection>(cf => cf.Collection)
            .WithMany(c => c.CollectionFeed)
            .HasForeignKey(cf => cf.CollectionId);

        }
    }
}
