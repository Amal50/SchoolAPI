using Newtonsoft.Json;
using RestApiClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;

namespace RestApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RestClient client = new RestClient("http://localhost:51125");
            IRestRequest restRequest = new RestRequest("api/Courses");
            IRestResponse response = client.Get(restRequest);

            Console.WriteLine(response.StatusCode);
              Console.WriteLine(response.Content);
            List<Courses> courses = JsonConvert.DeserializeObject<List<Courses>>(response.Content);

            foreach (var item in courses)
            {
                Console.WriteLine(item.CourseName);
            }


            //-------------------------
            IRestRequest restRequestGetById = new RestRequest("api/Courses/2");
            IRestResponse responseById = client.Get(restRequestGetById);

            Courses course = JsonConvert.DeserializeObject<Courses>(responseById.Content);
            Console.WriteLine("Single:" + course.CourseName);
        }
    }
}
