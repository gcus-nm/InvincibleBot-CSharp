namespace InvincibleBot.Core;

class Program
{
    private static void Main(string[] args) => new DiscordBotClient().MainAsync().GetAwaiter().GetResult();
}
