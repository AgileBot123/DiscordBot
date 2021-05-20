using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.API;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using ModBot.Bot.Modules;
using ModBot.Business.Services;
using ModBot.Domain.Interfaces;

namespace ChatFilterBot
{
    public class Program
    {
        public static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commandsServices;
        private IServiceProvider _services;
        // private Commands _BotCommands;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commandsServices = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<ICommandLogic, CommandLogic>()
                .AddSingleton(_client)
                .BuildServiceProvider();

           // _BotCommands = new Commands(new CommandLogic());

            string Token = "ODQ0NTM1Nzg5ODgyODM0OTU1.YKT1Pw.YFxf6PAcFRZws3hbx8YYE8KuLWs";

            _client.Log += _client_Log;
            await RegisterComamndsAsync();

            await _client.LoginAsync(TokenType.Bot, Token);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterComamndsAsync()
        {
            _client.MessageReceived += HandleCommandsAsync;
            await _commandsServices.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        }

        private async Task HandleCommandsAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos))
            {
                var result = await _commandsServices.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                

            }
        }


   
    }
}