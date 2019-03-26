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
            var feeds = db.Links.ToList();
            foreach (Feed feed in feeds)
            {
                UpdateCurrentFeed(feed.Id);
            }
         }

        private void UpdateCurrentFeed(double id)
        {
            Feed link = db.Links.FirstOrDefault(x => x.Id == id);
            List<NewsItem> oldItems = db.News.ToList();
            List<NewsItem> updateItems = new List<NewsItem>();
            var factory = new Feeds().ExecuteCreation(link.FeedType, link.Name);
            List<NewsItem> newsItems = (List<NewsItem>)factory.GetNews(link.Name);

            foreach (NewsItem item in newsItems)
            {
                if(!oldItems.Exists(x => x.Title == item.Title)){
                    updateItems.Add(item);
                }

            }

            link.News.AddRange(newsItems);
            db.SaveChanges();
        }
    }
}
