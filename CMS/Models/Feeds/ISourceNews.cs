using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public interface ISourceNews
    {
        IList<NewsItem> GetNews(string url);
        FeedType FeedType { get; }
    }

    public enum FeedType
    {
        RSS,
        Atom
    }
}
