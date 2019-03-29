using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using Xunit;
using Newtonsoft.Json;
using CMS;
using CMS.Models.Feeds;
using System.Text;

namespace XUnitTestCMS
{
    public class UnitTestControlerCMS
    {
        HttpClient httpClient;
        
        public UnitTestControlerCMS()
        {
            string curDir = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
            .SetBasePath(curDir)
            .AddJsonFile("appsettings.json");

            var server = new TestServer(new WebHostBuilder().UseContentRoot(curDir).UseConfiguration(builder.Build()).UseStartup<Startup>());
            httpClient = server.CreateClient();
        }

        [Fact]
        public async void TestControlerCMS()
        {
            // POST api/Collection  Create a new collection (returns collection Id)
            Collection collection = new Collection() { Name = "New Test collection" };
            HttpContent content1 = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(collection), Encoding.UTF8, "application/json");
            var request1 = await httpClient.PostAsync("api/collection", content1);
            Assert.Equal(System.Net.HttpStatusCode.OK, request1.StatusCode);
            var responce1 = request1.Content.ReadAsStringAsync().Result; 
            Assert.NotNull(responce1);


            // PUT api/Collection/5  Add feed to a collection
            int collectionId = int.Parse(responce1);
            Feed feed = new Feed() { Url = "http://www1.cbn.com/app_feeds/rss/news/rss.php?section=world", FeedType = FeedType.RSS };
            HttpContent content2 = new StringContent(JsonConvert.SerializeObject(feed), Encoding.UTF8, "application/json");
            var request2 = await httpClient.PutAsync("api/collection/"+ collectionId, content2);
            Assert.Equal(System.Net.HttpStatusCode.OK, request2.StatusCode);

            // repeat 
            var request3 = await httpClient.PutAsync("api/collection/" + collectionId, content2);
            Assert.Equal(System.Net.HttpStatusCode.OK, request3.StatusCode);
            var responce2 = request3.Content.ReadAsStringAsync().Result;
            Assert.Contains("Already exist", responce2);

            // bad rss(url)
            Feed badfeed = new Feed() { Url = "http://www1.cbn.com/app_feeds/badurl/news/rss.php?section=world", FeedType = FeedType.RSS };
            HttpContent content3 = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(badfeed), Encoding.UTF8, "application/json");
            var request4 = await httpClient.PutAsync("api/collection/" + collectionId, content3);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, request4.StatusCode);

            // GET api/Collection  Get all news for a collection{id}
            var collections = await httpClient.GetAsync("api/collection");
            Assert.Equal(System.Net.HttpStatusCode.OK, collections.StatusCode);
            var responce3 = collections.Content.ReadAsStringAsync().Result;
            Assert.Contains("New Test collection", responce3);

            // GET api/Collection/id  Get all news for a collection{id}
            var testcollection = await httpClient.GetAsync("api/collection/"  + collectionId);
            Assert.Equal(System.Net.HttpStatusCode.OK, collections.StatusCode);
            var responce4 = testcollection.Content.ReadAsStringAsync().Result;
            Assert.Contains("http://www.cbn.com/api", responce4);

        }
    }
}
