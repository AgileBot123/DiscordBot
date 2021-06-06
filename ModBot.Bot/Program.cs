using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using ModBot.Business.Services;
using ModBot.Domain.Interfaces;
using ModBot.DAL.Repository;
using ModBot.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModBot.Domain.Models;
using ModBot.Bot.Handler;
using System.Configuration;
using System.IO;

namespace ChatFilterBot
{
    public class Program
    {
        public static IConfigurationRoot Configuration;
        public static string Token { get; set; }
        public static string DatabaseString { get; set; }
        public static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            Token = configuration["DiscordToken:Token"];
            DatabaseString = configuration["ConnectionStrings:ModBotDatabase"];

            new Program().RunBotAsync().GetAwaiter().GetResult();
        }

        private DiscordSocketClient _client;
        private CommandService _commandsServices;
        private IServiceProvider _services;
        private BotHandler _botHandler;

        public DatabaseRepository DatabaseRepo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModBotContext>();
            optionsBuilder.UseSqlServer(DatabaseString);
            var _context = new ModBotContext(optionsBuilder.Options);
            var databaseRepo = new DatabaseRepository(_context);
            return databaseRepo;
        }
  
        public async Task RunBotAsync()
        {
      
            _client = new DiscordSocketClient();
            _commandsServices = new CommandService();

            _services = new ServiceCollection()
                 .AddSingleton(_client)
                 .AddDbContext<ModBotContext>(o => o.UseSqlServer(DatabaseString))
                 .AddScoped<ICommandLogic, CommandLogicService>()
                 .AddScoped<DatabaseRepository>()
                 .AddSingleton(_commandsServices)
                 .BuildServiceProvider();

            _botHandler = new BotHandler();

            _client.Log += _client_Log;
            await RegisterComamndsAsync();

            await _client.LoginAsync(TokenType.Bot, Token);

            await _client.StartAsync();
           
            await Task.Delay(-1);


        }

        public async Task LeftGuild(SocketGuild guild)
        {
            var databaseRepo = DatabaseRepo();
            var fetchedGuild = await databaseRepo.GetGuild(guild.Id);

            var update = new Guild(fetchedGuild.Id, false, fetchedGuild.Avatar, fetchedGuild.GuildName);

            databaseRepo.UpdateGuild(update);
        }

      

        public async Task JoinedGuild(SocketGuild guild)
        {
            var databaseRepo = DatabaseRepo();
            var fetchedGuild = await databaseRepo.GetGuild(guild.Id);

            if (fetchedGuild == null)
            {
                var createGuild = new Guild(guild.Id, true, guild.IconUrl, guild.Name);
                databaseRepo.CreateGuild(createGuild);
                var punishmentsLevels = await databaseRepo.GetPunishmentLevels(guild.Id);
                if (punishmentsLevels == null)
                {
                    punishmentsLevels = new PunishmentSettings(5, 15, 20, 5, 20, guild.Id);
                    databaseRepo.CreatePunishmentSetting(punishmentsLevels);
                }
            }
            else
            {
                var update = new Guild(fetchedGuild.Id, true, fetchedGuild.Avatar, fetchedGuild.GuildName);
                databaseRepo.UpdateGuild(update);
            }                   
        }



        private Task _client_Log(LogMessage arg)
            {
                Console.WriteLine(arg);
                return Task.CompletedTask;
            }


        public async Task RegisterComamndsAsync()
        {
            _client.MessageReceived += HandleCommandsAsync;
            _client.MessageReceived += _botHandler.CheckIfMessagesIsBannedWord;




            await _commandsServices.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            _client.LeftGuild += LeftGuild;
            _client.JoinedGuild += JoinedGuild;
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