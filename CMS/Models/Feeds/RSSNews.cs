using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Models.Feeds
{
    public class RSSNews : NewsItem
    {

        public RSSNews(string url)
        {
            Title = "";
            Content = "";
            PublishDate = DateTime.Now;
            FeedType = FeedType.RSS;
            Link = new Link { Name = url };
        }

        public RSSNews()
        {
            Title = "";
            Content = "";
            PublishDate = DateTime.Now;
            FeedType = FeedType.RSS;
        }

        public override IList<NewsItem> GetFeeds(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);
                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new RSSNews()
                              {
                                  FeedType = FeedType.RSS,
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = new Link { Name =  item.Elements().First(i => i.Name.LocalName == "link").Value },
                                  PublishDate = DateTime.Parse(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };
                return (IList<NewsItem>)entries.ToList();
            }
            catch
            {
                return new List<NewsItem>();
            }
        }

        public override bool IsExist(string url)
        {
            throw new NotImplementedException();
        }
    }
}
