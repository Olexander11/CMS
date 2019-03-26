using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public abstract class NewsItem
    {
        public long Id { get; set; }
        [Required]
        public Link Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public FeedType FeedType { get; set; }

        public abstract IList<NewsItem> GetFeeds(string url);
        public abstract bool IsExist(string url);
        
    }

    public enum FeedType {
    RSS,
    Atom
    }
}
