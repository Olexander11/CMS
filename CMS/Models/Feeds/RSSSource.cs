using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Models.Feeds
{
    public class RSSSource : ISourceNews
    {

        public FeedType FeedType
        {
            get
            {
                return FeedType.RSS;
            }
        }

        public IList<NewsItem> GetNews(Feed feed)
        {
            try
            {
                XDocument doc = XDocument.Load(feed.Url);
                List<NewsItem> entries = new List<NewsItem>();
                var items = doc.Descendants("item");
                foreach (XElement item in items)
                {
                    entries.Add(new NewsItem()
                    {
                        Link = FillParams("link", item),
                        Content = FillParams("description", item),
                        PublishDate = DateTime.TryParse(FillParams("pubDate", item), out DateTime validValue) ? validValue : (DateTime?)null,
                        Title = FillParams("title", item),
                        Feed = feed
                    });

                }


                return (IList<NewsItem>)entries.ToList();
            }
            catch(Exception ex)
            {
                return new List<NewsItem>();
            }


        }

        private string FillParams(string text, XElement item)
        {
            XElement result = item.Elements().FirstOrDefault(i => i.Name.LocalName == text);
            return result == null ? "" : result.Value;
        }

    }
}
