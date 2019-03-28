using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace CMS.Models.Feeds
{
    public  class NewsItem
    {
        public long Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublishDate { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Feed Feed { get; set; }


    }

    
}
