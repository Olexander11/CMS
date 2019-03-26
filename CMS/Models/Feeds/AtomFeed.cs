using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Models.Feeds
{
    public class AtomFeed : Feed
    {
        public AtomFeed()
        {
            Title = "";
            Content = "";
            PublishDate = DateTime.Now;
            FeedType = FeedType.Atom;
        }

        public AtomFeed(string url)
        {
            Title = "";
            Content = "";
            PublishDate = DateTime.Now;
            FeedType = FeedType.Atom;
            Link = new Link { Name = url };
        }

        public override IList<Feed> GetFeeds(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);
                var entries = from item in doc.Root.Elements().Where(i => i.Name.LocalName == "entry")
                              select new AtomFeed()
                              {
                                  FeedType = FeedType.Atom,
                                  Content = item.Elements().First(i => i.Name.LocalName == "summary").Value,
                                  Link = new Link { Name = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value },
                                  PublishDate = DateTime.Parse(item.Elements().First(i => i.Name.LocalName == "updated").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };
                return (IList<Feed>)entries.ToList();
            }
            catch
            {
                return new List<Feed>();
            }
        }

        public override bool IsExist(string url)
        {
            throw new NotImplementedException();
        }
    }
}
