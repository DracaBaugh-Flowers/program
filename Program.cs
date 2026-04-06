// Build a simple C# app that calls a Python REST API (e.g., FastAPI).

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://127.0.0.1:8000/");

        try
        {
            // GET all items
            HttpResponseMessage getResponse = await client.GetAsync("items");
            getResponse.EnsureSuccessStatusCode();
            string getResult = await getResponse.Content.ReadAsStringAsync();
            Console.WriteLine("GET /items:");
            Console.WriteLine(getResult);

            // POST a new item
            var newItem = new { name = "Laptop", price = 1200 };
            string json = JsonSerializer.Serialize(newItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await client.PostAsync("items", content);
            postResponse.EnsureSuccessStatusCode();
            string postResult = await postResponse.Content.ReadAsStringAsync();
            Console.WriteLine("POST /items:");
            Console.WriteLine(postResult);

            // GET the first item
            HttpResponseMessage singleResponse = await client.GetAsync("items/0");
            singleResponse.EnsureSuccessStatusCode();
            string singleResult = await singleResponse.Content.ReadAsStringAsync();
            Console.WriteLine("GET /items/0:");
            Console.WriteLine(singleResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error:");
            Console.WriteLine(ex.Message);
        }
    }
}