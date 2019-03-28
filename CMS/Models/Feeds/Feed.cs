using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class Feed
    {
        public long Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public FeedType FeedType { get; set; }
        public List<NewsItem> News { get; set; }

        public IList<CollectionFeed> CollectionFeed { get; set; }

    }
}
