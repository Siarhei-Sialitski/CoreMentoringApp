using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreMentoringApp.ConsoleRestClient.Models;
using Newtonsoft.Json;

namespace CoreMentoringApp.ConsoleRestClient
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:44346/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string userInput = string.Empty;
            do
            {
                if (!string.IsNullOrEmpty(userInput))
                {
                    switch (userInput.ToLower())
                    {
                        case "p":
                        {
                            ShowList(await GetAsync<IEnumerable<Product>>("products"));
                            break;
                        }
                        case "c":
                        {
                            ShowList(await GetAsync<IEnumerable<Category>>("categories"));
                            break;
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("<----------------------------------------------------->");
                    Console.WriteLine();
                }

                userInput = GetUserInput();

            } while (!userInput.Equals("E", StringComparison.OrdinalIgnoreCase));
        }

        static string GetUserInput()
        {
            Console.WriteLine("Choose command:");
            Console.WriteLine("P - get all products");
            Console.WriteLine("C - get all categories");
            Console.WriteLine("E - exit");
            return Convert.ToString(Console.ReadLine());
        }

        static void ShowList<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }
        }

        static async Task<T> GetAsync<T>(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<T>();
        }
        
    }
}
