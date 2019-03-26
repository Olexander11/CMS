using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Models.Feeds
{
    public class AtomNews : NewsItem
    {
        public AtomNews()
        {
            Title = "";
            Content = "";
            Link = "";
            PublishDate = DateTime.Now;
        }

        public AtomNews(string url)
        {
            Title = "";
            Content = "";
            Link = "";
            PublishDate = DateTime.Now;
        }

        public override IList<NewsItem> GetNews(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);
                var entries = from item in doc.Root.Elements().Where(i => i.Name.LocalName == "entry")
                              select new AtomNews()
                              {
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  Content = item.Elements().First(i => i.Name.LocalName == "summary").Value,
                                  PublishDate = DateTime.Parse(item.Elements().First(i => i.Name.LocalName == "updated").Value),
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
