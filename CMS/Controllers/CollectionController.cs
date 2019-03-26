using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models.Feeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    public class CollectionController : Controller
    {
        FeedsContext db;
        private IMemoryCache cache;

        public CollectionController(FeedsContext context, IMemoryCache memoryCache)
        {
            this.db = context;
            cache = memoryCache;
        }

        // GET api/id Collection  Get all news for a collection{id}
        [HttpGet("{id}")]
        public IActionResult Get(double id)
        {
            CollectionFeeds feeds = null;
            if (!cache.TryGetValue(id, out feeds))
            {

                feeds = db.Collections.Include(s => s.CollFeeds).FirstOrDefault(x => x.Id == id);
                if (feeds == null) return NotFound();
                cache.Set(feeds.Id, feeds,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30))); // Store cache 30 min
            }

            return new ObjectResult(feeds.CollFeeds.ToList());
        }


        // POST api/Collection  Create a new collection (returns collection Id)
        [HttpPost]
        public IActionResult Post([FromBody]CollectionFeeds collection)
        {
            if (collection == null)
            {
                return BadRequest();
            }

            db.Collections.Add(collection);
            db.SaveChanges();

            return Ok(collection.Id);
        }

        // PUT api/Collection/5  Add feed to a collection
        [HttpPut]
        public IActionResult Put(int id, [FromBody]Feed link)
        {
            if (link == null)
            {
                return BadRequest();
            }

            CollectionFeeds feeds = db.Collections.Include(s => s.CollFeeds).FirstOrDefault(x => x.Id == id);
            if (feeds == null) return NotFound();

            var factory = new Feeds().ExecuteCreation(link.FeedType, link.Name);
            List<NewsItem> newsItems = (List<NewsItem>) factory.GetNews(link.Name);
            feeds.CollFeeds.Add(link);
            int n = db.SaveChanges();
            if (n > 0)
            {
                cache.Set(feeds.Id, feeds, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
            }




            return Ok();
        }

       
    }
}
