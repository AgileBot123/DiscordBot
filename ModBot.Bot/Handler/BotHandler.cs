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
        public BotHandler()
        {
            var databaseRepo = DatabaseRepo();
            commandLogicService = new CommandLogicService(databaseRepo);
        }

        public async Task CheckIfMessagesIsBannedWord(SocketMessage message)
        {
            var IsBannedWord = await commandLogicService.CheckBannedWordsFromFile(message.Content, 838707761067982888);

            if (IsBannedWord)
            {
                await message.Channel.SendMessageAsync("Not allowed word");
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
            var userStrikes = await commandLogicService.GetUserStrikes(user.Guild.Id, user.Id);
            var punishmentSettings = await punishmentsLevelsService.GetPunishmentLevels(user.Guild.Id);

            switch(userStrikes)
            {
                case var x when x >= punishmentSettings.BanLevel:
                    BanMember(user, message);
                    break;

                case var x when x >= punishmentSettings.KickLevel:
                    KickMember(user, message);
                    break;

                case var x when x >= punishmentSettings.TimeOutLevel:
                    await TimeOutMember(user, message, punishmentSettings.StrikeMuteTime);
                    break;

                default:
                    break;
            }
        }

        public void BanMember(SocketGuildUser user, SocketMessage message)
        {
            user.Guild.AddBanAsync(user);
            user.SendMessageAsync($"You have been banned for the following message: {message}");

        }

        public void KickMember(SocketGuildUser user, SocketMessage message)
        {
            user.KickAsync();
            user.SendMessageAsync($"You have been kicked for the following message: {message}");
        }

        public async Task TimeOutMember(SocketGuildUser user, SocketMessage message, int time)
        {
           var roleId =  await commandLogicService.CreateMuteRole(user.Guild);
           await commandLogicService.MuteMember(user, time, roleId);
           await user.SendMessageAsync($"You have been muted for {time} amount of seconds, for the following message: {message}");
        }


        #endregion
    }
}
