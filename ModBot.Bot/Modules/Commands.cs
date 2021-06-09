
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

        [Command("UserStrikes")]
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
            {
                await ReplyAsync(response);
            }
        }



        [Command("ResetServerStrikes", RunMode = RunMode.Async)]
        public async Task ResetAllStrike()
        {
            var commandUser = Context.User as SocketGuildUser;

            if (commandUser.GuildPermissions.Administrator)
            {
                var request = new ConfirmationBuilder()
                                .WithContent(new PageBuilder().WithText("Are you sure you wanna do this?"))
                                                .Build();

                var result = await InteractivityService.SendConfirmationAsync(request, Context.Channel);

                if (result.Value)
                {
                    await Context.Channel.SendMessageAsync("Confirmed :thumbsup:!");
                    await _commandLogic.ResetAllStrikes(Context.Guild.Id);
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Declined :thumbsdown:!");
                }
            }
            else
            {
                await ReplyAsync("You don't have permission to use this command");
            }         
        }

        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("RemoveStrikes")]
        public async Task RemoveStrike(SocketGuildUser user, int amount)
        {
            var commandUser = Context.User as SocketGuildUser;
            if (commandUser.GuildPermissions.Administrator)
            {
                await _commandLogic.AddMemberToDatabase(user.Id, user.Username, user.GetAvatarUrl(), user.IsBot, Context.Guild.Id);
                await _commandLogic.RemoveStrike(amount, user.Id, Context.Guild.Id);
            }
            else
            {
                await ReplyAsync("You don't have permission to use this command");
            }         
        }

        [Command("AddStrikes")]

        public async Task AddStrike(SocketGuildUser user, int amount)
        {
            var commandUser = Context.User as SocketGuildUser;
            if (commandUser.GuildPermissions.Administrator)
            {
                await _commandLogic.AddMemberToDatabase(user.Id, user.Username, user.GetAvatarUrl(), user.IsBot, Context.Guild.Id);
                await _commandLogic.AddStrikeToUser(amount, user.Id, Context.Guild.Id);
            }
            else
            {
                await ReplyAsync("You don't have permission to use this command");
            }
        }
        

        [Command("Mute", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task Mute (SocketGuildUser user, int time)
        {
            var commandUser = Context.User as SocketGuildUser;
            if (commandUser.GuildPermissions.Administrator)
            {
               var roleId =  await _commandLogic.CreateMuteRole(Context.Guild);
               await _commandLogic.MuteMember(user, time, roleId);

            }
            else
            {
                await ReplyAsync("You don't have permission to use this command");
            }
          
        }

        [Command("Help")]
        public async Task Help()
        {
            await ReplyAsync("These are the commands available: " + "\n" + 
                             "!Ping       ->  Will healthcheck the bot" + "\n" + 
                             "!Userstrikes ->  Check your strikes"   + "\n" +
                             "!AdminHelp  ->  To check admin commnads");
        }

        
        [Command("AdminHelp")]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task AdminHelp()
        {
            await ReplyAsync("These are the commands available: " + "\n" +
                             "!Mute               ->  To mute a specific user and time in minutes.    Example !mute " + Context.User.Username + " 10" + "\n" +
                             "!Addstrikes         ->  To add strikes to specific user.                         Example !addstrikes " + Context.User.Username + " 10" + "\n" +
                             "!Removestrikes      ->  To remove strikes from a specific user.      Example !removestrikes " + Context.User.Username + " 5" + "\n" +
                             "!ResetServerStrikes ->  To reset all members strikes on the server");
        }

    }
}
