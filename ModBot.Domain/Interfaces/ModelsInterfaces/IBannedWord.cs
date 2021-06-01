using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ModelsInterfaces
{
    public interface IBannedWord
    {
        string Profanity { get;  }
        int Strikes { get; }
        string Punishment { get;  }
        int BannedWordUsedCount { get; }
        ulong GuildId { get; }
        Guild Guild { get; }
    }
}
