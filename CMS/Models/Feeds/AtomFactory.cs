using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class AtomFactory : FeedFactory
    {
        public override Feed Create(string url)
        {
            return new AtomFeed(url);
        }
    }
}
