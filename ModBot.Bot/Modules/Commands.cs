
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
            await AddMemberToDatabase(Context.User);

            var response = _commandLogic.BotResponseCooldown(Context);
            if (response != null)
                await ReplyAsync(response);
            else
                await ReplyAsync("pong");          
        }

        private async Task AddMemberToDatabase(IUser user)
        {
            await _commandLogic.AddMemberToDatabase(user.Id, user.Username, user.GetAvatarUrl(), "test@gmail.com", user.IsBot, Context.Guild.Id);
        }

        [Command("UserStrike")]      
        public async Task UserStrike(SocketGuildUser inputedUser = default)
        {
            var user = inputedUser == null ? (SocketGuildUser)Context.User : inputedUser;

            if (inputedUser == null || (inputedUser != null && user.GuildPermissions.Administrator))
            {
                await AddMemberToDatabase(user);

                var response = await _commandLogic.GetUserStrikes(user.Id, Context.Guild.Id);
                await ReplyAsync(response.ToString());
            }

            //var user = inputedUser == null ? Context.User : inputedUser;

            //await AddMemberToDatabase(user);

            //var response = await _commandLogic.GetUserStrikes(user.Id, Context.Guild.Id);
            //await ReplyAsync(response.ToString());
        }

        [Command("Strike")] //checkar själva användares egna strikes

        public async Task Strike(SocketMessage arg)
        {
            await AddMemberToDatabase(Context.User);
        }

        [Command("RemoveStrike")]

        public async Task RemoveStrike()
        {
            await AddMemberToDatabase(Context.User);
        }

        [Command("AddStrike")]

        public async Task AddStrike()
        {
        
        }
    }
}
