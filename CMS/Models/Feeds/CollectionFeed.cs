using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class CollectionFeed
    {
        public long CollectionId { get; set; }
        public Collection Collection { get; set; }

        public long FeedId { get; set; }
        public Feed Feed { get; set; }

    }
}
