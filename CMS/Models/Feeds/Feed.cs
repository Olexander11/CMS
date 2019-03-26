using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public abstract class Feed
    {
        public long Id { get; set; }
        public Link Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public FeedType FeedType { get; set; }

        public abstract IList<Feed> GetFeeds(string url);
        public abstract bool IsExist(string url);
        
    }

    public enum FeedType {
    RSS,
    Atom
    }
}
