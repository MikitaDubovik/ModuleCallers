using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace ModuleCallerConsole
{
    class Program
    {
        static HttpClient client = new HttpClient();
        private static string GetCategories = "categories";
        private static string GetProducts = "products";

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:49855/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            dynamic categories = null;
            
            HttpResponseMessage response = await client.GetAsync(GetCategories);
            if (response.IsSuccessStatusCode)
            {
                categories = await ReadAsJsonAsync<dynamic>(response.Content);
            }

            foreach (var category in categories)
            {
                Console.WriteLine("Category ID - " + category.categoryId);
                Console.WriteLine("Category Name - " + category.categoryName);
                Console.WriteLine();
            }

            dynamic products = null;
            response = await client.GetAsync(GetProducts);
            if (response.IsSuccessStatusCode)
            {
                categories = await ReadAsJsonAsync<dynamic>(response.Content);
            }

            foreach (var category in categories)
            {
                Console.WriteLine("Product ID - " + category.productId);
                Console.WriteLine("Product Name - " + category.productName);
                Console.WriteLine();
            }
        }

        static async Task<T> ReadAsJsonAsync<T>(HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }
    }
}
