using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using ModBot.Business.Services;
using ModBot.DAL.Data;
using ModBot.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Bot.Handler
{
    public class BotHandler : ModuleBase<SocketCommandContext>
    {
        public CommandLogicService commandLogicService;
        public PunishmentsLevelsService punishmentsLevelsService;
        private DiscordSocketClient _client;
        public BotHandler()
        {
            _client = new DiscordSocketClient();
            var databaseRepo = DatabaseRepo();
            punishmentsLevelsService = new PunishmentsLevelsService(databaseRepo);
            commandLogicService = new CommandLogicService(databaseRepo);
        }

        public async Task CheckIfMessagesIsBannedWord(SocketMessage message)
        {
            var user = message as SocketUserMessage;
            var context = new SocketCommandContext(_client, user);

            if (!message.Author.IsBot)
            {
                var IsBannedWord = await commandLogicService.CheckBannedWordsFromUsersMessage(user, context.Guild.Id);

                if (IsBannedWord)
                {
                    await message.DeleteAsync();
                    await HandleMemeberStrikes(message);
                }
            }
        }

        private static DatabaseRepository DatabaseRepo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModBotContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ModBotDatabase;Trusted_Connection=True");
            var _context = new ModBotContext(optionsBuilder.Options);
            var databaseRepo = new DatabaseRepository(_context);
            return databaseRepo;
        }

        #region GrantDivinePunishment

        public async Task HandleMemeberStrikes(SocketMessage message)
        {
            var user = message.Author as SocketGuildUser;
            var userStrikes = await commandLogicService.GetUserStrikes(user.Id, user.Guild.Id);
            var punishmentSettings = await punishmentsLevelsService.GetPunishmentLevels(user.Guild.Id);

            switch(userStrikes)
            {
                case var x when x >= punishmentSettings.BanLevel:
                    await BanMember(user, message);
                    break;

                case var x when x >= punishmentSettings.KickLevel:
                    await KickMember(user, message);
                    break;

                case var x when x >= punishmentSettings.TimeOutLevel:
                    await TimeOutMember(user, message, punishmentSettings.StrikeMuteTime);
                    break;

                default:
                    break;
            }
        }

        public async Task BanMember(SocketGuildUser user, SocketMessage message)
        {
            await user.SendMessageAsync($"You have been banned for the following message: {message}");

            await user.Guild.AddBanAsync(user);          
        }

        public async Task KickMember(SocketGuildUser user, SocketMessage message)
        {
            await user.SendMessageAsync($"You have been kicked for the following message: {message}");
            await user.KickAsync();
          
        }

        public async Task TimeOutMember(SocketGuildUser user, SocketMessage message, int time)
        {
           var roleId =  await commandLogicService.CreateMuteRole(user.Guild);
            await user.SendMessageAsync($"You have been muted for {time} of seconds, for the following message: {message}");
            await commandLogicService.MuteMember(user, time, roleId);
        }


        #endregion
    }
}
