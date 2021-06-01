using Discord.Commands;
using Discord.WebSocket;
using ModBot.DAL.Repository;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{

    public class CommandLogicService : ModuleBase<SocketCommandContext> , ICommandLogic
    {
        public static List<DateTimeOffset> stackCooldownTimer = new List<DateTimeOffset>();
        public static List<SocketGuildUser> stackCooldownTarget = new List<SocketGuildUser>();

        private readonly DatabaseRepository _databaseRepository;
        public CommandLogicService(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }



        public async Task<int> GetUserStrikes(ulong memberID, ulong guildId)
        {
            var Punishment = await GetPunishment(memberID, guildId);
            return Punishment.StrikesAmount;
        }

        // Ska vi flytta nedan metod till datalageret. Den uppfyller CRUD.
        private async Task<Punishment> GetPunishment(ulong memberID, ulong guildId)
        {
            var Allmemberpunishment = await _databaseRepository.GetAllMemberPunishments();
            var memberPunishmentIdList = Allmemberpunishment.Where(x => x.MemberId == memberID)
                                                        .Select(mp => mp.PunishmentId).ToList();

            var AllguildPunishments = await _databaseRepository.GetAllGuildPunishments();
            var guildPunishmentIdList = AllguildPunishments.Where(x => x.GuildId == guildId)
                                                       .Select(gp => gp.PunishmentId).ToList();

            var AllPunishments = await _databaseRepository.GetAllPunishments();
            var PunishmentID = guildPunishmentIdList.Intersect(memberPunishmentIdList).FirstOrDefault();
            var Punishment = AllPunishments.Where(p => p.Id == PunishmentID).FirstOrDefault();
            return Punishment;
        }

        public async Task AddMemberToDatabase(ulong memberId, string username, string avatar, string email, bool isBot, ulong guildId)
        {
            var allMembers = await _databaseRepository.GetAllMembers();

            if (!allMembers.Any(x => x.Id == memberId))
            {
                var createMember = new Member(memberId, username, avatar, email, isBot);
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

        public void AddStrikeToUser(int amount, ulong UserId, ulong GuildId)
        {
            throw new NotImplementedException();
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
    }
}
