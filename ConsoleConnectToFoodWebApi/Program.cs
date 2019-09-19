using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectToFoodWebApi
{
    class Program
    {
        public class Food
        {
            public int ID;
            public string Name;
            public int Calories;
            public string Ingridients;
            public int Grade;
        }

        private const string URL = "http://localhost:64047/api/food/search?name=p";

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            // -------------------------- Get One item
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                Console.Write("{0} ", dataObjects);
                //Console.Write("{0} ", dataObjects.Name);
                //Console.Write("{0} ", dataObjects.Calories);
                //Console.WriteLine("{0}", dataObjects.Ingridients);
                //Console.WriteLine("{0}", dataObjects.Grade);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // -------------------------- Post One item
            Task t = Task.Run(() => PostInConsoleApp());
            t.Wait(); 

            //client.Dispose();
            Console.ReadKey();
        }

        private static async void PostInConsoleApp()
        {
            HttpResponseMessage response;
            HttpClient client = new HttpClient();
            string URL = "http://localhost:64047/api/food";
            client.BaseAddress = new Uri(URL);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string payload = "{\"Name\": \"Pad Thai Erez321\",\"Calories\": 500,\"Ingridients\": \"tamarind Erez2\",\"Grade\": 8}";
            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            Uri u = new Uri(URL);
            response = await client.PostAsync(u, httpContent);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            
            if (response.IsSuccessStatusCode)
            {
                //var dataObjects = response.Content.ReadAsStringAsync().Result;
                Console.Write(response.StatusCode.ToString());

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

        }
    }
}
