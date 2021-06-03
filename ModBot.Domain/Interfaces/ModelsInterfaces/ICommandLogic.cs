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

        void AddStrikeToUser(int amount, ulong UserId, ulong GuildId);
        string BotResponseCooldown(SocketCommandContext context);
        Task AddMemberToDatabase(ulong UserId, string username, string avatar, string email, bool isBot, ulong guildId);
        Task MuteMember(SocketGuildUser user, int time, ulong roleID);
        Task<ulong> CreateMuteRole(SocketGuild guild);
    }
}
