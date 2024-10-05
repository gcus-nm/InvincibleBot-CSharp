using System.Text;
using Newtonsoft.Json;

namespace BotClient.Core
{
    public static class SlashCommandRegister
    {
        /// <summary>
        /// スラッシュコマンドの登録
        /// </summary>
        public static async void Add(Dictionary<string, object> registerJsonCommands)
        {
            string url = $"https://discord.com/api/v8/applications/1116820846678380614/commands";
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string content = JsonConvert.SerializeObject(registerJsonCommands);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Headers.Add("Authorization", $"Bot {DiscordBotClient.BotToken}");
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.SendAsync(request);
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}