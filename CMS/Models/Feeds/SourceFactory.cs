using System;
using System.Collections.Generic;

namespace CMS.Models.Feeds
{
    public sealed class SourceFactory
    {
        private static volatile SourceFactory _instance;
        private static readonly object InstanceLoker = new Object();

        private readonly Dictionary<FeedType, ISourceNews> _sources;
                
        private  SourceFactory()
        {
            _sources = new Dictionary<FeedType, ISourceNews>();
            var type = typeof(ISourceNews);
            foreach (Type t in typeof(SourceFactory).Assembly.GetTypes())
            {
                if (type.IsAssignableFrom(t) && t.IsClass)
                {
                    var source = (ISourceNews)Activator.CreateInstance(t);

                    // var source = (ISourceNews)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(t); 
                    _sources[source.FeedType] = source;
                }

            }
        }

        public static SourceFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLoker)
                    {
                        if (_instance == null)
                            _instance = new SourceFactory();
                    }
                }

                return _instance;
            }
        }

        public ISourceNews GetSourceNews(FeedType type)
        {
            return _sources[type];
        }
    }
}
