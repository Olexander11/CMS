using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class Collection
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public IList<CollectionFeed> CollectionFeed { get; set; }

    }
}
