using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces
{
    public interface IPunishmentsLevels
    {
        int Id { get;  }
        int TimeOutLevel { get; }
        int KickLevel { get;  }
        int BanLevel { get; }
        DateTime SpamMuteTime { get; }
        DateTime StrikeMuteTime { get; }
    }
}
