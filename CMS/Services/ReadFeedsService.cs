using CMS.Models;
using CMS.Models.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class ReadFeedsService : ITimeService
    {
        private int _timeHour;
        FeedsContext db;
        FileLogger _logger;

        public int TimeIntervalHours
        {
            get { return _timeHour; }
            set { _timeHour = value; }
        }

        public ReadFeedsService(FeedsContext context)
        {
            this.db = context;
        }

     

        public void DoService()
        {
            try
            {
              db = new FeedsContext();
              var feeds = db.Feeds.ToList();


                foreach (Feed feed in feeds)
                {
                    var oldNews = db.News.Where(x => x.Feed == feed).ToList();
                    if (oldNews.Count != 0) { db.News.RemoveRange(oldNews); }
                                       
                    var newsItems = SourceFactory.Instance.GetSourceNews(feed.FeedType).GetNews(feed);
                    if (newsItems.Count == 0) continue;
                    db.News.AddRange(newsItems);
                }
                db.SaveChanges();
            }
            catch
            { }

        }

                
    }
}
