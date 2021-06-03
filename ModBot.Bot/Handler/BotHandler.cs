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
        public CommandLogicService service;
        
        public BotHandler()
        {
            var databaseRepo = DatabaseRepo();
            service = new CommandLogicService(databaseRepo);
        }

        public async Task CheckIfMessagesIsBannedWord(SocketMessage message)
        {
            var IsBannedWord = await service.CheckBannedWordsFromFile(message.Content, 838707761067982888);

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
    }
}
