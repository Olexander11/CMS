using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models.Feeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    public class CollectionController : Controller
    {
        FeedsContext db;

        public CollectionController(FeedsContext context)
        {
            this.db = context;
        }

        // GET api/id Collection  Get all news for a collection{id}
        [HttpGet("{id}")]
        public IActionResult Get(double id)
        {
            CollectionFeeds feeds = db.Collections.Include(s => s.CollFeeds).FirstOrDefault(x => x.Id == id);
            if (feeds == null) return NotFound();

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
        public IActionResult Put(int id, [FromBody]Link link)
        {
            if (link == null)
            {
                return BadRequest();
            }

            CollectionFeeds feeds = db.Collections.Include(s => s.CollFeeds).FirstOrDefault(x => x.Id == id);
            if (feeds == null) return NotFound();

            var factory = new Feed().ExecuteCreation(link.FeedType, link.Name);
            List<NewsItem> newsItems = (List<NewsItem>) factory.GetFeeds(link.Name);
            feeds.CollFeeds.AddRange(newsItems);
            db.SaveChanges();
            return Ok();
        }

       
    }
}
