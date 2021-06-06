
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            await _commandLogic.AddMemberToDatabase(user.Id, user.Username, user.GetAvatarUrl(), user.IsBot, Context.Guild.Id);
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



        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ResetServerStrikes")]

        public async Task ResetAllStrike()
        {  
             await _commandLogic.ResetAllStrikes();
        }

        [Command("RemoveStrike")]

        public async Task RemoveStrike(SocketGuildUser user, int amount)
        {
            await _commandLogic.AddMemberToDatabase(user.Id, user.Username, user.GetAvatarUrl(), user.IsBot, Context.Guild.Id);

            await _commandLogic.RemoveStrike(amount, user.Id, Context.Guild.Id);
        }

        [Command("AddStrike")]

        public async Task AddStrike(SocketGuildUser user, int amount)
        {
            await _commandLogic.AddMemberToDatabase(user.Id, user.Username, user.GetAvatarUrl(), user.IsBot, Context.Guild.Id);

            await _commandLogic.AddStrikeToUser(amount, user.Id, Context.Guild.Id);
        }

        [Command("Mute")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task Mute (SocketGuildUser user, int time)
        {
            var roleId = await _commandLogic.CreateMuteRole(Context.Guild);
            await _commandLogic.MuteMember(user, time, roleId);
        }

    }
}
