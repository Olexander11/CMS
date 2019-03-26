using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class CollectionFeeds
    {
        public double Id { get; set; }
        public string Name { get; set; }
        public List<Feed> CollFeeds { get; set; }
    }
}
