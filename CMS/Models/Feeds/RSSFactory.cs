using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class RSSFactory : FeedFactory
    {
        public override NewsItem Create(string url)
        {
            return new RSSNews(url);
        }
    }
}
