using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class AtomFactory : FeedFactory
    {
        public override NewsItem Create(string url)
        {
            return new AtomNews(url);
        }
    }
}
