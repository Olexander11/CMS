using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class Feed
    {
        private readonly Dictionary<FeedType, FeedFactory> _factories;

        public Feed()
        {
            foreach (FeedType type in Enum.GetValues(typeof(FeedType)))
            {
                var factory = (FeedFactory)Activator.CreateInstance(Type.GetType("FactoryMethod." + Enum.GetName(typeof(FeedType), type) + "Factory"));
                _factories.Add(type, factory);
            }
        }
        // to execute var factory = new Feed().ExecuteCreation(FeedType.RSS, "url");
        // factory.GetFeeds();
        public NewsItem ExecuteCreation(FeedType type, string url) => _factories[type].Create(url);


    }
}
