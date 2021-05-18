using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Chat_Filter_Bot.Modules;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Chat_Filter_Bot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();


        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().AddSingleton(client).AddSingleton(commands).BuildServiceProvider();

            string token = "ODM2OTI3NTYwMTg0NjI3MjQx.YIlHhw.nafuSmUCmVSqQqaUnkg3eP5Bvvk";

            client.Log += Client_Log; ;

            await RegisterCommandsAsync();

            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(client, message);
            if (message.Author.IsBot)
                return;

            int argPos = 0;
            if(message.HasStringPrefix("!", ref argPos))
            {
                var result = await commands.ExecuteAsync(context, argPos, services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }

            var blacklist = new ProfanityBlackList();

            foreach (string word in blacklist.Bannable)
            {
                if (message.Content.Contains(word))
                {
                    await message.DeleteAsync();
                    await message.Author.SendMessageAsync("You were banned for using profanity from our banlist.");
                    
                    await context.Guild.AddBanAsync(message.Author);
                }
            }

            foreach (string word in blacklist.Kickable)
            {
                if (message.Content.Contains(word))
                {
                    await message.DeleteAsync();
                    await message.Author.SendMessageAsync("You were kicked for using profanity from our kicklist.");

                    IGuildUser user = (IGuildUser)message.Author;
                    await user.KickAsync();
                }
            }

            foreach (string word in blacklist.Deletable)
            {
                if (message.Content.Contains(word))
                {
                    await message.DeleteAsync();
                    await message.Author.SendMessageAsync("Your message was deleted because it was profanity.");
                }
            }
        }
    }
}
