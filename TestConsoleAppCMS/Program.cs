using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleAppCMS
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:50563/");


            // post
            var request = new RestRequest("api/collection", Method.POST);
            request.AddParameter("Name", "New collection 1"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Console.WriteLine("We create new collection. Id = " + content);

            // put RSS
            //var request1 = new RestRequest("api/collection", Method.PUT);
            //request1.AddParameter("Url", "https://www.feedforall.com/blog-feed.xml");
            //request1.AddParameter("FeedType", "RSS");
            //IRestResponse response1 = client.Execute(request1);
            //content = response.Content;
            //Console.WriteLine("We create new collection. Id = " + content);

            Console.ReadKey();
        }
    }
}
