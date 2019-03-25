using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    public class CollectionController : Controller
    {
        // GET api/Collection  Get all news for a collection
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST api/Collection  Create a new collection (returns collection Id)
        [HttpPost]
        public int Post([FromBody]string value)
        {
            return { };
        }

        // PUT api/Collection/5  Add feed to a collection
        [HttpPut]
        public void Put([FromBody]string value)
        {
        }

       
    }
}
