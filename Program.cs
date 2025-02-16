using System.Text.Json;
using System.Text.Json.Nodes;

namespace Http
{
    class Program
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

        public static async Task Main()
        {
            _ = BuddyLoop();
            Console.WriteLine("Hello World I'm impatient");

            await BuddyLoop();
            Console.WriteLine("I'm patient");

            await FetchAndSaveJson();
        }

        public static async Task FetchAndSaveJson()
        {
            string? url = "https://api.genderize.io?name=Drage";
            string? filePath = "response.json";
            try
            {
                using HttpClient client = new();

                string data = await client.GetStringAsync(url);

                var prettyJson1 = JsonSerializer.Serialize(JsonSerializer.Deserialize<object>(data), JsonOptions);
                Console.WriteLine(prettyJson1);

                var prettyJson2 = JsonNode.Parse(data)?.ToJsonString(JsonOptions);
                Console.WriteLine(prettyJson2);

                var prettyJson3 = JsonSerializer.Serialize(JsonDocument.Parse(data).RootElement, JsonOptions);
                await File.WriteAllTextAsync(filePath, prettyJson3);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        public static async Task BuddyLoop()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Hello from loop {i}");
                await Task.Delay(50);
            }
        }
    }
}
