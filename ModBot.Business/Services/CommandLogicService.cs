using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ModBot.DAL.FileSaving;
using ModBot.DAL.Repository;
using ModBot.Domain.DTO.BannedWordDtos;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{

    public class CommandLogicService : ModuleBase<SocketCommandContext> , ICommandLogic
    {
        public static List<DateTimeOffset> stackCooldownTimer = new List<DateTimeOffset>();
        public static List<SocketGuildUser> stackCooldownTarget = new List<SocketGuildUser>();
        public static List<SocketGuildUser> MutedMemeberList = new List<SocketGuildUser>();


        private FileSaving _fileSaving;
        private readonly DatabaseRepository _databaseRepository;
        public CommandLogicService(DatabaseRepository databaseRepository)
        {
            _fileSaving = new FileSaving();
            _databaseRepository = databaseRepository;
            
        }


        public async Task<int> GetUserStrikes(ulong memberID, ulong guildId)
        {
            var Punishment = await GetPunishment(memberID, guildId);
            return Punishment.StrikesAmount;
        }


        private async Task<Punishment> GetPunishment(ulong memberID, ulong guildId)
        {
            var Allmemberpunishment = await _databaseRepository.GetAllMemberPunishments();
            var memberPunishmentIdList =  Allmemberpunishment.Where(x => x.MemberId == memberID)
                                                        .Select(mp => mp.PunishmentId).ToList();

            var AllguildPunishments = _databaseRepository.GetAllGuildPunishments();
            var guildPunishmentIdList =  AllguildPunishments.Where(x => x.GuildId == guildId)
                                                       .Select(gp => gp.PunishmentId).ToList();

            var AllPunishments =  await _databaseRepository.GetAllPunishmentsAsync();
            var PunishmentID = guildPunishmentIdList.Intersect(memberPunishmentIdList).FirstOrDefault();
            var Punishment =  AllPunishments.Where(p => p.Id == PunishmentID).FirstOrDefault();
            return Punishment;
        }

        public async Task AddMemberToDatabase(ulong memberId, string username, string avatar, bool isBot, ulong guildId)
        {
            var allMembers = await _databaseRepository.GetAllMembers();

            if (!allMembers.Any(x => x.Id == memberId))
            {
                var createMember = new Member(memberId, username, avatar, isBot);
                _databaseRepository.AddMember(createMember);
            }

            bool MissingPunishment = await GetPunishment(memberId, guildId) == null;

            if (MissingPunishment)
            {
                var punishment = await _databaseRepository.CreatePunishment();

                if (punishment != null)
                {
                    await _databaseRepository.AddToMemberPunishment(memberId, punishment.Id);

                    await _databaseRepository.CreateGuildPunishment(punishment.Id, guildId);
                }
            }                
        }
                                                       
        public async Task<bool> AddStrikeToUser(int amount, ulong UserId, ulong guildId)
        {
            var guildPunishmentsList = _databaseRepository.GetAllGuildPunishments();
            var getGuildPunishmentID = guildPunishmentsList.Where(x => x.GuildId == guildId).Select(x => x.PunishmentId).ToList();

            var getMemberList = await _databaseRepository.GetAllMemberPunishments();
            var selectspecifcmember = getMemberList.Where(x => x.MemberId == UserId).Select(x => x.PunishmentId).ToList();

            var membersp = getGuildPunishmentID.Intersect(selectspecifcmember).FirstOrDefault();




            var punishmentList = await _databaseRepository.GetAllPunishmentsAsync();
            var Punishment = punishmentList.Where(p => p.Id == membersp).FirstOrDefault();

            Punishment.StrikesAmount += amount;
            //TODO ADD TIMEOUTUNTIL
            var result = await _databaseRepository.UpdatePunishment(Punishment);

            if (result)           
                return true;
            
            return false;
        }

        


        public string BotResponseCooldown(SocketCommandContext context)
        {
            //Check if your user list contains who just used that command.
            if (stackCooldownTarget.Contains(context.User as SocketGuildUser))
            {
                //If they have used this command before, take the time the user last did something, add 5 seconds, and see if it's greater than this very moment.
                if (stackCooldownTimer[stackCooldownTarget.IndexOf(context.Message.Author as SocketGuildUser)].AddSeconds(5) >= DateTimeOffset.Now)
                {
                    //If enough time hasn't passed, reply letting them know how much longer they need to wait, and end the code.
                    int secondsLeft = (int)(stackCooldownTimer[stackCooldownTarget.IndexOf(context.Message.Author as SocketGuildUser)].AddSeconds(5) - DateTimeOffset.Now).TotalSeconds;
                    return $"Hey! You have to wait at least {secondsLeft} seconds before you can use that command again!";
                }
                else
                {
                    //If enough time has passed, set the time for the user to right now.
                    stackCooldownTimer[stackCooldownTarget.IndexOf(context.Message.Author as SocketGuildUser)] = DateTimeOffset.Now;
                    return null;
                }
            }
            else
            {
               var member = context.User as SocketGuildUser;
                if (!member.GuildPermissions.Administrator)
                {
                    //If they've never used this command before, add their username and when they just used this command.
                    stackCooldownTarget.Add(context.User as SocketGuildUser);
                    stackCooldownTimer.Add(DateTimeOffset.Now);
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<ulong> CreateMuteRole(SocketGuild guild)
        {

            bool rExist = false;
            ulong roleID = 0;
            foreach (var gRole in guild.Roles)
            {
                if (gRole.Name.Equals("Muted"))
                {
                    rExist = true;
                    roleID = gRole.Id;
                    break;
                }
            }

            if (!rExist)
            {
                //if the roles doesnt exist u create it and set the perms of the channels
                var mRole = await guild.CreateRoleAsync(
                    "Muted", Discord.GuildPermissions.None,
                    Discord.Color.DarkTeal/*what ever color*/, false, null
                    );

                roleID = mRole.Id;


                try
                {
                    foreach (var channel in guild.Channels)
                    {
                        await channel.AddPermissionOverwriteAsync(mRole,
                        OverwritePermissions.DenyAll(channel).Modify(
                        viewChannel: PermValue.Allow, readMessageHistory: PermValue.Allow)
                        );
                    }
                }
                catch (Exception e)
                {
                    //handel error if occures
                }
          
            }
            return roleID;                   
        }

        public async Task<int> GetMuteTime(ulong guild)
        {
            var punishmentSettings = await _databaseRepository.GetPunishmentLevels(guild);
            return punishmentSettings.SpamMuteTime;
        }

        public async Task MuteMember(SocketGuildUser user, int time, ulong roleID)
        {
            await Task.Run(() =>
            {
                Task.Delay(10000);
                var role = user.Guild.GetRole(roleID);
                user.AddRoleAsync(role);
                MutedList(user, time, role).GetAwaiter();
            });            
        }
        private async Task<string> MutedList(SocketGuildUser user, int time, SocketRole role)
        {
            time *= 1000 * 60;

            if(!MutedMemeberList.Contains(user))
            {
                MutedMemeberList.Add(user);

                var MuteCooldown = Task.Run(async delegate
                {
                    await Task.Delay(time);
                    await user.RemoveRoleAsync(role);
                    MutedMemeberList.Remove(user);

                });
                try
                {
                    MuteCooldown.GetAwaiter();
                }
                catch(Exception ex)
                {
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                return $"user already muted";
            }
            return null;
        }

        public async Task<string> CheckBannedWordsFromUsersMessage(SocketUserMessage message, ulong guildId)
        {
            var allBannedWords = _fileSaving.LoadFromFile<BannedWordForFileDto>(guildId);

            string sentence = message.Content;
            string[] words = sentence.Split(' ');
            List<string> bannedWords = new List<string>();
            bool hasBannedword = false;
            string specificWordPunishment = "";
            int strikeValue = 0;
            bool IsSpecificWord = false;

            for (int i = 0; i < words.Length; i++)
            {
                if (allBannedWords.Any(x => x.Profanity.ToString().ToLower() == words[i].ToLower()))
                {
                    hasBannedword = true;
                    bannedWords.Add(words[i]);
                }  
            }

            if(hasBannedword)
            {
                
                for (int i = 0; i < bannedWords.Count; i++)
                {
                    IsSpecificWord = allBannedWords.Any(x => x.Profanity.ToString().ToLower() ==
                                                           bannedWords[i].ToLower() && x.GuildId == guildId);
                    
                    specificWordPunishment = allBannedWords.Where(x => x.Profanity.ToString().ToLower() ==
                                                 bannedWords[i].ToLower() && x.GuildId == guildId)
                                                         .Select(x => x.Punishment).FirstOrDefault();

                    strikeValue = allBannedWords.Where(x => x.Profanity.ToString().ToLower() ==
                                               bannedWords[i].ToString().ToLower() && x.GuildId == guildId)
                                                               .Select(x => x.Strikes).FirstOrDefault();
                }

                if (IsSpecificWord)
                {
                    await AddMemberToDatabase(message.Author.Id, message.Author.Username, message.Author.AvatarId, message.Author.IsBot, guildId);

                    var strikeAdded = await AddStrikeToUser(strikeValue, message.Author.Id, guildId);

                    if (strikeAdded)
                        return specificWordPunishment;
                }      
            }
            return null;
        }


        public async Task<bool> RemoveStrike(int amount, ulong UserId, ulong guildID)
        {
            var guildPunishmentsList = _databaseRepository.GetAllGuildPunishments();
            var getGuildPunishmentID = guildPunishmentsList.Where(x => x.GuildId == guildID).Select(x => x.PunishmentId).ToList();

            var getMemberList = await _databaseRepository.GetAllMemberPunishments();
            var selectspecifcmember = getMemberList.Where(x => x.MemberId == UserId).Select(x => x.PunishmentId).ToList();

            var membersp = getGuildPunishmentID.Intersect(selectspecifcmember).FirstOrDefault();


            var punishmentList = await _databaseRepository.GetAllPunishmentsAsync();
            var punishment = punishmentList.Where(p => p.Id == membersp).FirstOrDefault();

            if (amount > punishment.StrikesAmount)
            {
                punishment.StrikesAmount -= punishment.StrikesAmount;
            } 
            else
            {
                punishment.StrikesAmount -= amount;
            }


            var result = await _databaseRepository.UpdatePunishment(punishment);

            if (result)
                return true;

            return false;
        }

        public async Task ResetAllStrikes(ulong guildId)
        {
            var Allpunishments =  _databaseRepository.GetAllPunishments();
            var AllGuildPunishments = _databaseRepository.GetAllGuildPunishments();
            var PunishmentIdList = AllGuildPunishments.Where(x => x.GuildId == guildId).Select(z => z.PunishmentId);

            List<Punishment> punishmentList = new List<Punishment>();
            foreach (var ID in PunishmentIdList)
            {
                punishmentList.Add(Allpunishments.Where(x => x.Id == ID).FirstOrDefault());
            }

            foreach (var user in punishmentList)
            {
                user.StrikesAmount = 0;
               await _databaseRepository.UpdatePunishment(user);
            }        

        }

        public async Task<int> GetStrikeMuteTime(ulong guildId)
        {
            var strikeMuteTime = await _databaseRepository.GetPunishmentLevels(guildId);

            return strikeMuteTime.StrikeMuteTime;
        }
    }
}