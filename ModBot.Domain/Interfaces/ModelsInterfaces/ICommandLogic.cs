using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces
{
    public interface ICommandLogic
    {
        Member GetUserStrikes(ulong UserID);

        string BotResponseCooldown(SocketCommandContext context);
        
    }
}
