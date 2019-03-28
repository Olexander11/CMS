using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Models.Feeds
{
    public class AtomSource : ISourceNews
    {
        
        public FeedType FeedType
        {
            get
            {
                return FeedType.Atom;
            }
        }

        public  IList<NewsItem> GetNews(Feed feed)
        {
            try
            {
                XDocument doc = XDocument.Load(feed.Url);
                List<NewsItem> entries = new List<NewsItem>();
                XNamespace xNamespace = "http://www.w3.org/2005/Atom";
                var items = doc.Root.Descendants(xNamespace+"entry");
               
                foreach (XElement item in items)
                {
                    entries.Add(new NewsItem()
                    {
                        Link = FillParams("link", item),
                        Content = FillParams("summary", item),
                        PublishDate = DateTime.TryParse(FillParams("updated", item), out DateTime validValue) ? validValue : (DateTime?)null,
                        Title = FillParams("title", item),                        
                        Feed = feed
                    });
                    
                }
               
                return (IList<NewsItem>)entries.ToList();
            }
            catch
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
