
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

            var response = _commandLogic.BotResponseCooldown(Context);
            if (response == null)
                await ReplyAsync("pong");
            else
                await ReplyAsync(response);          
        }

        private async Task AddMemberToDatabase(IUser user)
        {
            await _commandLogic.AddMemberToDatabase(user.Id, user.Username, user.GetAvatarUrl(), "test@gmail.com", user.IsBot, Context.Guild.Id);
        }

        [Command("UserStrike")]      
        public async Task UserStrike(SocketGuildUser inputedUser = default)
        {
            var response = _commandLogic.BotResponseCooldown(Context);
            if (response == null)
            {
                var commandUser = Context.User as SocketGuildUser;
                var userToCheck = inputedUser == null ? Context.User : inputedUser;

                if (inputedUser == null || (inputedUser != null && commandUser.GuildPermissions.Administrator))
                {
                    await AddMemberToDatabase(userToCheck);

                    var strikes = await _commandLogic.GetUserStrikes(userToCheck.Id, Context.Guild.Id);
                    await ReplyAsync(strikes.ToString());
                }
                else
                {
                    await ReplyAsync("You don't have premission to check other members strikes");
                }
            }
            else
                await ReplyAsync(response);
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
