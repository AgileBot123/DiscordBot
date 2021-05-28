﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ModBot.DAL.Repository;
using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IMember> GetUserStrikes(ulong UserID) => await _databaseRepository.GetMember(UserID);

        public async Task<bool> AddMemberToDatabase(ulong UserId, string username, string avatar, string email, bool isBot)
        {
            var allMembers = await _databaseRepository.GetAllMembers();

            if (!allMembers.Any(x => x.Id == UserId) || allMembers.Count() == 0)
            {
               var createMember = new Member(UserId,username,avatar,email,isBot);
               var result =  _databaseRepository.AddMember(createMember);

                if (result)
                    return true; 
            }
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
                   //If they've never used this command before, add their username and when they just used this command.
                   stackCooldownTarget.Add(context.User as SocketGuildUser);
                   stackCooldownTimer.Add(DateTimeOffset.Now);
                   return null;
               }
        }
    }
}
