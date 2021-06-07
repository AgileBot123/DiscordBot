using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces
{
    public interface IPunishmentsLevels
    {
        int TimeOutLevel { get;  }
        int KickLevel { get; }
        int BanLevel { get; }
        int SpamMuteTime { get;  }
        int StrikeMuteTime { get; }
        ulong GuildId { get; }
        Guild Guild { get; }
    }
}
