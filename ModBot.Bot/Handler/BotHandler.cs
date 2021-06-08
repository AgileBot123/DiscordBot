using ChatFilterBot;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using ModBot.Business.Services;
using ModBot.DAL.Data;
using ModBot.DAL.Repository;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Bot.Handler
{
    public class BotHandler : ModuleBase<SocketCommandContext>
    {
        public CommandLogicService commandLogicService;
        public PunishmentsLevelsService punishmentsLevelsService;
        private DiscordSocketClient _client;
        public Program program;
        private List<AntiSpamModel> userCooldownList = new List<AntiSpamModel>();
        public BotHandler()
        {
            _client = new DiscordSocketClient();
            program = new Program();
            punishmentsLevelsService = new PunishmentsLevelsService(program.DatabaseRepo());
            commandLogicService = new CommandLogicService(program.DatabaseRepo());
        }
        public async Task CheckIfMessagesIsBannedWord(SocketMessage message)
        {
            if (!string.IsNullOrEmpty(message.Content))
            {
                if (!message.Author.IsBot)
                {
                    var user = message as SocketUserMessage;
                    var context = new SocketCommandContext(_client, user);

                    var punishmentValue = await commandLogicService.CheckBannedWordsFromUsersMessage(user, context.Guild.Id);

                    if (!string.IsNullOrEmpty(punishmentValue))
                    {
                        await message.DeleteAsync();

                        var result = await WordSpecificPunish(user, punishmentValue, context);

                        if (!result)
                        {
                            await HandleMemeberStrikes(message);
                        }
                    }
                }
            }          
        }

        public async Task AntiSpam(SocketMessage message)
        {
            if (!string.IsNullOrEmpty(message.Content))
            {
                if (!message.Author.IsBot)
                {
                    var user = message as SocketUserMessage;
                    var userGuild = message.Author as SocketGuildUser;
                    var context = new SocketCommandContext(_client, user);


                    if (!userCooldownList.Any(x => x.User == userGuild))
                    {
                        var antispamModel = new AntiSpamModel
                        {
                            User = userGuild,
                            Counter = 0,
                            Timer = DateTimeOffset.Now,
                            TempMessage = message.Content
                        };
                        userCooldownList.Add(antispamModel);
                    }
                    else
                    {
                      var antiSpamTemp = userCooldownList.Where(x => x.User == userGuild).Single();
                       antiSpamTemp.TempMessage = message.Content;
                    }

                    if (userCooldownList.Any(x => x.TempMessage == message.Content))
                    {
                        foreach (var usersInfo in userCooldownList.Where(x => x.User == userGuild).ToList())
                        {
                            if (usersInfo.Timer >= DateTimeOffset.Now)
                            {
                                usersInfo.Counter++;

                                if (usersInfo.Counter >= 3)
                                {
                                    var roleId = await commandLogicService.CreateMuteRole(userGuild.Guild);
                                    var spamMuteTime = await commandLogicService.GetMuteTime(context.Guild.Id);
                                    await commandLogicService.MuteMember(userGuild, spamMuteTime, roleId);
                                    await message.DeleteAsync();
                                }
                            }
                            else
                            {
                                usersInfo.Counter = 0;
                                usersInfo.Timer = DateTimeOffset.Now.AddSeconds(20);
                            }
                        }
                    }
                }
            }    
        }


        #region GrantDivinePunishment

        private async Task<bool> WordSpecificPunish(SocketMessage message, string wordPunish, SocketCommandContext context)
        {
            var user = message.Author as SocketGuildUser;

            switch (wordPunish)
            {
                case var x when x.Equals("Ban"):
                    await BanMember(user, message);
                    return true;

                case var x when x.Equals("Kick"):
                    await KickMember(user, message);
                    return true;

                case var x when x.Equals("Mute"):
                    await TimeOutMember(user, message, await commandLogicService.GetStrikeMuteTime(context.Guild.Id));
                    return true;

                default:
                    return false;
            }
        }

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
            await user.SendMessageAsync($"You have been muted for {time} minutes, for the following message: {message}");
            await commandLogicService.MuteMember(user, time, roleId);
        }
        #endregion
    }
}
