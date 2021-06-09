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
        Task<int> GetUserStrikes(ulong memberID, ulong guildId);
        Task<bool> AddStrikeToUser(int amount, ulong UserId, ulong guildId);
        string BotResponseCooldown(SocketCommandContext context);
        Task AddMemberToDatabase(ulong UserId, string username, string avatar, bool isBot, ulong guildId);
        Task MuteMember(SocketGuildUser user, int time, ulong roleID);
        Task<ulong> CreateMuteRole(SocketGuild guild);
        Task<bool> RemoveStrike(int amount, ulong UserId, ulong guilId);
        Task ResetAllStrikes(ulong guildId);
        Task<int> GetStrikeMuteTime(ulong guildId);
        Task<int> GetMuteTime(ulong guild);
    }
}
