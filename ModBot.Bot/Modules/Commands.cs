
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
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
            AddMemberToDatabase();

            var response = _commandLogic.BotResponseCooldown(Context);
            if (response != null)
                await ReplyAsync(response);
            else
                await ReplyAsync("pong");          
        }

        private void AddMemberToDatabase()
        {
            _commandLogic.AddMemberToDatabase(Context.User.Id, Context.User.Username, Context.User.GetAvatarUrl(), "test@.gmail.com", Context.User.IsBot, Context.Guild.Id);
        }

        [Command("UserStrike")]
        
        public async Task UserStrike()
        {
      
        }

        [Command("Strike")] //checkar själva användares egna strikes

        public async Task Strike(SocketMessage arg)
        {     
                var response = _commandLogic.GetUserStrikes(arg.Author.Id);
                await ReplyAsync(response.Id.ToString());    
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
