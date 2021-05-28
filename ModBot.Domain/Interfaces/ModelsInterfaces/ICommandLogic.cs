using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ModBot.Domain.interfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces
{
    public interface ICommandLogic
    {
        Task<IMember> GetUserStrikes(ulong UserID);

        string BotResponseCooldown(SocketCommandContext context);
        Task<bool> AddMemberToDatabase(ulong UserId, string username, string avatar, string email, bool isBot);


    }
}
