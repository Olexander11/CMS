using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public interface ISourceNews
    {
        IList<NewsItem> GetNews(Feed feed);
        FeedType FeedType { get; }
    }

    public enum FeedType
    {
        RSS = 1,
        Atom = 2
    }
}
