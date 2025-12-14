using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Threading;

namespace CustomizeAPIClient
{
    internal class ClientGenerator
    {
        public async Task GenerateClient()
        {
            try
            {
                var httpClient = new HttpClient();
                var swaggerJson = await httpClient.GetStringAsync("https://localhost:7284/swagger/v1/swagger.json");
                var document = await OpenApiDocument.FromJsonAsync(swaggerJson);

                var settings = new CSharpClientGeneratorSettings
                {
                    ClassName = "BlogApiClient", // Fixed typo: CLient → Client
                    CSharpGeneratorSettings =
                    {
                        Namespace = "BlogAPI"
                    }
                };

                var generator = new CSharpClientGenerator(document, settings);
                var code = generator.GenerateFile();

                // Use full path and ensure directory exists
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "BlogApiClient.cs");
                var directory = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }


                File.WriteAllText(filePath, code);

                Console.WriteLine($"Client generated successfully at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating client: {ex.Message}");
                throw; // Re-throw to see the actual error
            }
        }
    }
}
