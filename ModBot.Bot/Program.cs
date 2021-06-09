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
using Interactivity;
using System.Linq;

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
                 .AddScoped<InteractivityService>()
                 .AddSingleton(new InteractivityConfig { DefaultTimeout = TimeSpan.FromSeconds(20) })
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
            

  
            _client.MessageReceived += _botHandler.AntiSpam;


            await _commandsServices.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            _client.LeftGuild += LeftGuild;
            _client.JoinedGuild += JoinedGuild;
        }

        private async Task HandleCommandsAsync(SocketMessage arg)
        {
            if (arg is SocketUserMessage message)
            {
                var context = new SocketCommandContext(_client, message);
                if (message.Author.IsBot) return;

                int argPos = 0;
 
                if (message.HasStringPrefix("!", ref argPos))
                {
                    var result = await _commandsServices.ExecuteAsync(context, argPos, _services);
                    if (result.ErrorReason != null)
                        switch (result.ErrorReason)
                        {
                            //If bots roles is underneath mute role.
                            case "The server responded with error 403: Forbidden":
                                await context.Channel.SendMessageAsync("Bots role is underneath Mute role, please move boxbot role up above the muted role in the server settings");
                                    break;
                            //Not enough paramaters
                            case "The input text has too few parameters.":
                                await context.Channel.SendMessageAsync(
                                    "This command lacks parameters. Check the command description for more details.");
                                break;
                            //Bad command
                            case "Unknown command.":
                                await context.Channel.SendMessageAsync(
                                    "I don't understand this command.");
                                break;
                            //Some other shenanigans
                            default:
                                await context.Channel.SendMessageAsync(
                                    $"{result.ErrorReason}");
                                break;
                        }
                }
            }
        }
     }     
}