using Discord.WebSocket;
using System.Runtime.InteropServices;

namespace BotClient.Core
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
	}
}
