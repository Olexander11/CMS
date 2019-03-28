using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models.Feeds;
using CMS.Services;
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
        public IActionResult Get(long id)
        {
            List<Feed> feeds = db.CollectionFeed.Where(x => x.CollectionId == id).Select(x =>x.Feed).ToList();


            if (feeds == null) return NotFound();
            List<NewsItem> result = new List<NewsItem>();
            foreach (Feed feed in feeds)
            {
                List<NewsItem> news = db.Feeds.SelectMany(x => x.News).ToList();
                if (news.Count <= 0) continue;
                result.AddRange(news);
            }

           // var respond = result.Select(x => new { Title = x.Title, Content = x.Content, Date = x.PublishDate, Link = x.Link });

            return new ObjectResult(result);
        }


        // POST api/Collection  Create a new collection (returns collection Id)
        [HttpPost]
        public IActionResult Post([FromBody]Collection collection)
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
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Feed feed)
        {
            if (feed == null)
            {
                return BadRequest();
            }

            Collection collection = db.Collections.Include(c => c.CollectionFeed).FirstOrDefault(x => x.Id == id);
            if (collection == null) return NotFound();

            CollectionFeed collectionFeed = new CollectionFeed() { Collection = collection, Feed = feed };
            if (collection.CollectionFeed == null)
            {
                collection.CollectionFeed = new List<CollectionFeed>();
            }
            var feeddb = db.Feeds.FirstOrDefault(x => x.Url == feed.Url);
            if (feeddb != null)
            {
                if (collection.CollectionFeed.Any(x => x.FeedId == feeddb.Id)) return Ok("Already exist");
                collectionFeed = new CollectionFeed() { Collection = collection, Feed = feeddb };
                collection.CollectionFeed.Add(collectionFeed);
            }
            else
            {
                var newsItems = SourceFactory.Instance.GetSourceNews(feed.FeedType).GetNews(feed);
                if (newsItems.Count == 0) return BadRequest("Wrong feed!");

                collection.CollectionFeed.Add(collectionFeed);
                db.News.AddRange(newsItems);
            }

            db.SaveChanges();
            return Ok("Ok");

        }
    }
}
