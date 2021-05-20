using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ModBot.Business.Services;
using ModBot.Domain.Interfaces;

namespace ModBot.Bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        private readonly ICommandLogic _commandLogic;
        public Commands(ICommandLogic commandLogic)
        {
            _commandLogic = commandLogic;
        }


        [Command("ping")]
        public async Task Ping()
        {
            var response = _commandLogic.BotResponseCooldown(Context);
            if (response != null)
                await ReplyAsync(response);
            else
                await ReplyAsync("pong");
        }

        [Command("UserStrike")]
        
        public async Task UserStrike()
        {
      
        }

        [Command("Strike")] //checkar själva användares egna strikes

        public async Task Strike(SocketMessage arg)
        {
            var response = _commandLogic.GetUserStrikes(arg.Author.Id);
            await ReplyAsync(response);
        }

        [Command("RemoveStrike")]

        public async Task RemoveStrike()
        {
        
        }

        [Command("AddStrike")]

        public async Task AddStrike()
        {
        
        }
    }
}
