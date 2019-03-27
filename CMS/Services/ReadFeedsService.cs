using CMS.Models.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class ReadFeedsService
    {
        FeedsContext db;
        public ReadFeedsService (FeedsContext context)
        {
            this.db = context;
           
        }
        public ReadFeedsService()
        {
           
        }

        public void ReloadAllNews() {
            var feeds = db.Feeds.ToList();
            foreach (Feed feed in feeds)
            {
                UpdateCurrentFeed(feed.Id);
            }
         }

        private void UpdateCurrentFeed(long id)
        {
            Feed link = db.Feeds.FirstOrDefault(x => x.Id == id);
            List<NewsItem> oldItems = link.News.ToList();
            List<NewsItem> updateItems = new List<NewsItem>();
            var newsItems = SourceFactory.Instance.GetSourceNews(link.FeedType).GetNews(link.Url);

            foreach (NewsItem item in newsItems)
            {
                if (!oldItems.Exists(x => x.Title == item.Title))
                {
                    updateItems.Add(item);
                }
            }

            link.News.AddRange(updateItems);
            db.SaveChanges();
        }
    }
}
