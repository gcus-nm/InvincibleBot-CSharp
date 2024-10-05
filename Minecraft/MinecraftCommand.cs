using Discord;
using Discord.Commands;
using Discord.Commands.Builders;
using Discord.WebSocket;
using InvincibleBot.Core;

namespace InvincibleBot.Minecraft
{   
    public class MinecraftCommand : ModuleBase
    {
        protected override void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);

            DiscordBotClient.Instance.BotClient.Ready += async () => await Regist();
            DiscordBotClient.Instance.BotClient.SlashCommandExecuted += OnSlashCommandExecuted;
        }

        /// <summary>
        /// スラッシュコマンドの登録
        /// </summary>
        /// <returns></returns>
        private async Task Regist()
        {
            SlashCommandBuilder builder = new SlashCommandBuilder()
                .WithName("mine")
                .WithDescription("Minecraft関連のコマンド")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithType(ApplicationCommandOptionType.SubCommand)
                    .WithName("start")
                    .WithDescription("サーバーを起動します")
                ).AddOption(new SlashCommandOptionBuilder()
                    .WithType(ApplicationCommandOptionType.SubCommand)
                    .WithName("stop")
                    .WithDescription("サーバーを停止します")
                );

            try
            {
                var guild = DiscordBotClient.Instance.BotClient.GetGuild(951653786307395605);
                await guild.CreateApplicationCommandAsync(builder.Build());
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// スラッシュコマンドが実行されたときの処理
        /// </summary>
        /// <param name="command">コマンドの内容</param>
        /// <returns></returns>
        private async Task OnSlashCommandExecuted(SocketSlashCommand command)
        {
            if (command.Data.Name != "mine")
            {
                return;
            }

            switch(command.Data.Options.First().Name)
            {
                case "start":
                    await Start(command);
                    break;
                case "stop":
                    await Stop(command);
                    break;
                default:
                    await command.RespondAsync("コマンドを処理できませんでした。");
                    break;
            }
        }

        /// <summary>
        /// サーバーを起動する
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private async Task Start(SocketSlashCommand command)
        {
            await command.RespondAsync("サーバーを起動します");
        }

        /// <summary>
        /// サーバーを停止する
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private async Task Stop(SocketSlashCommand command)
        {
            await command.RespondAsync("サーバーを停止します");
        }
    }
}