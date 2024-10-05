using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;

namespace InvincibleBot.Core
{
	public class DiscordBotClient
	{
		private readonly string BOT_TOKEN_FILEPATH = $"Resources/BotToken.txt";

		public static DiscordBotClient Instance { get; private set; }

		public DiscordSocketClient BotClient { get; private set; }
		public CommandService BotCommandService { get; private set; }
		public Microsoft.Extensions.DependencyInjection.ServiceProvider BotServices { get; private set; }
		public IMessageChannel ResentMessageChannel { get; private set; }
		public static string BotToken { get; private set; }

		public DiscordBotClient()
		{
			if (Instance != null)
			{
				throw new InvalidOperationException($"{nameof(DiscordBotClient)}インスタンスは1つしか生成できません");
			}

			Instance = this;
		}

		~DiscordBotClient()
		{
			BotClient.Dispose();
		}

		public async Task MainAsync()
		{
			var config = new DiscordSocketConfig
			{
				GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
			};

			BotClient = new DiscordSocketClient(config);
			BotCommandService = new CommandService();
			BotServices = new ServiceCollection().BuildServiceProvider();

			try
			{
				using (StreamReader read = new StreamReader(BOT_TOKEN_FILEPATH, Encoding.UTF8))
				{
					BotToken = read.ReadLine() ?? "invaild_token";
				}

				if (string.IsNullOrEmpty(BotToken))
				{
					Console.WriteLine("トークン取得失敗");
					throw new InvalidOperationException();
				}

				Console.WriteLine("Bot起動開始！");
				await BotCommandService.AddModulesAsync(Assembly.GetEntryAssembly(), BotServices);
				await BotClient.LoginAsync(TokenType.Bot, BotToken);
				await BotClient.StartAsync();

				await Task.Delay(-1);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Console.ReadLine();
			}
		}
	}
}