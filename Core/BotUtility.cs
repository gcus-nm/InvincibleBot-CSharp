using Discord.WebSocket;
using System.Runtime.InteropServices;

namespace InvincibleBot.Core
{
	public static class BotUtility
	{
		/// <summary>
		/// メッセージが有効かどうか
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public static bool IsValidMessage(SocketMessage message)
		{
			if (message == null)
			{
				return false;
			}

			if (message.Author.IsBot)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 実行環境を返す
		/// </summary>
		/// <returns></returns>
		public static OSPlatform GetServerOS()
		{
			var servers = new OSPlatform[3]
			{
				OSPlatform.Windows,
				OSPlatform.OSX,
				OSPlatform.Linux
			};

			for (int i = 0; i < servers.Length; ++i)
			{
				if (RuntimeInformation.IsOSPlatform(servers[i]))
				{
					return servers[i];
				}
			}

			throw new PlatformNotSupportedException();
		}

		/// <summary>
		/// サーバーIDを元にサーバーのオブジェクトを取得<br/>
		/// ※<see cref="DiscordSocketClient.Ready"/>の呼び出しが完了している必要があります
		/// </summary>
		/// <param name="guildIds"></param>
		/// <returns></returns>
		public static SocketGuild?[] GetGuilds(params ulong[] guildIds)
		{
			return guildIds.Select(guildId => DiscordBotClient.Instance.BotClient.GetGuild(guildId)).ToArray();
		}
	}
}
