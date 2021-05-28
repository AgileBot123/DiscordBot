using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ModelsInterfaces
{
    public interface IBannedWordGuilds
    {
         ulong GuildId { get;  }
         Guild Guild { get; }
         string BannedWordProfanity { get; }
         BannedWord BannedWord { get;}
    }
}
