using NSwag;
using NSwag.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BlogAPI;

namespace CustomizeAPIClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var apiBaseUrl = "https://localhost:7284";

            var client = new BlogApiClient(apiBaseUrl, httpClient);

            try
            {
                // Call the method on the client instance
                var blogs = await client.BlogsGETAsync(CancellationToken.None);
                foreach (var blog in blogs)
                {
                    Console.WriteLine($"{blog.Title}: {blog.Body}");
                }
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"API Error: {ex.StatusCode} - {ex.Message}");
            }
        }
    }
}
