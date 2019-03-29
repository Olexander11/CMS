using RestSharp;
using System;
using System.Net;


namespace TestConsoleAppCMS
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:57578/api/");

            // api/collection get
            Console.WriteLine("We make GER  request to localhost:57578/api/collection");
            var request = new RestRequest("collection", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Console.WriteLine("We find collections " + content);



            // api/post  post
            Console.WriteLine("We try POST  request to localhost:57578/api/collection");
            var requestPost = new RestRequest("collection", Method.POST);
            requestPost.RequestFormat = DataFormat.Json;
            Console.Write("Input new collection name: ");
            var name = Console.ReadLine();
            // create new collection item
            requestPost.AddJsonBody(
               new 
               {
                   Name = name
               });


            var responsePost = client.Execute(requestPost);

            var contentPost = responsePost.Content;
            int collectionId = int.Parse(contentPost);
            if (responsePost.StatusCode == HttpStatusCode.OK)
            {
               
                
                Console.WriteLine("We create new collection. Id = " + collectionId);
            }
            else
            {
                Console.WriteLine("Something wrong... ");
            }

            // PUT api/Collection/5  Add feed to a collection
            Console.WriteLine("We try PUT  request to localhost:57578/api/collection/" + collectionId);
            var requestPut = new RestRequest("collection/" + collectionId,  Method.PUT);
            requestPut.RequestFormat = DataFormat.Json;
            Console.Write("Input url, that you want to add to collection "+ name+" :");
            var url = Console.ReadLine();
            Console.Write("Input feed type ( 1- RSS, 2- Atom) :");
            var type = Console.ReadLine();
            // create new feed item
            requestPut.AddJsonBody(
               new
               {
                   Url = url,
                   FeedType = type
               });
            var responsetPut = client.Execute(requestPut);
            var contentPut = responsetPut.Content;
            Console.WriteLine("Put operation ctatus : " + contentPut);



            // api/collection/id get
            Console.WriteLine("We make GER  request to localhost:57578/api/collection/" + collectionId);
            var client1 = new RestClient("http://localhost:57578/api/");
            var request1 = new RestRequest("collection/" + collectionId, Method.GET);
            IRestResponse response1 = client.Execute(request1);
            var content1 = response1.Content;
            Console.WriteLine("We find news in new collection where must be news from new feed ");
            Console.WriteLine(content1);
            Console.ReadKey();
        }
    }
}
