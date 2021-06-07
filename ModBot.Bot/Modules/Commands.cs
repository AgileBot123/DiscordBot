
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Interactivity;
using Interactivity.Confirmation;
using ModBot.Domain.Interfaces;

namespace ModBot.Bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        public InteractivityService InteractivityService { get; set; }


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
        [Command("ResetServerStrikes", RunMode = RunMode.Async)]
        public async Task ResetAllStrike()
        {
            var request = new ConfirmationBuilder()
                                 .WithContent(new PageBuilder().WithText("Are you sure you wanna do this?"))
                                                 .Build();

            var result = await InteractivityService.SendConfirmationAsync(request, Context.Channel);

            if (result.Value)
            {
                await Context.Channel.SendMessageAsync("Confirmed :thumbsup:!");
                await _commandLogic.ResetAllStrikes();
            }
            else
            {
                await Context.Channel.SendMessageAsync("Declined :thumbsup:!");
            }
        }

        [RequireUserPermission(GuildPermission.Administrator)]
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
