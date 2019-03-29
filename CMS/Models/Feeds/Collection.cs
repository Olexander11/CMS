using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CMS.Models.Feeds
{
    public class Collection
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public IList<CollectionFeed> CollectionFeed { get; set; }

    }
}
