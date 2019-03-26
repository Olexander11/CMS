using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public abstract class FeedFactory
    {
        public abstract Feed Create(string url);
    }
}
