using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces
{
    public interface ICommandLogic
    {
        string GetUserStrikes(ulong UserID);
        
    }
}
