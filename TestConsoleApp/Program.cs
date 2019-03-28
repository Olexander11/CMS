using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:50562/");

            // post
            var requestPost = new RestRequest("api/collection", Method.POST);
            requestPost.AddParameter("Name", "Collection 1"); // adds to POST or URL querystring based on Method
            //requestPost.AddUrlSegment("id", "123"); // replaces matching token in request.Resource
            
            IRestResponse response = client.Execute(requestPost);
           Console.WriteLine("Responce from post "+response.Content); // raw content as string
        }
    }
}
