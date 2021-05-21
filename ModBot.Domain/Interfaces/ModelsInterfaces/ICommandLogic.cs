using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces
{
    public interface ICommandLogic
    {
        string GetUserStrikes(ulong UserID);

        string BotResponseCooldown(SocketCommandContext context);
        
    }
}
