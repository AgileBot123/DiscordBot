using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ModelsInterfaces
{
    public interface IGuildStatistics
    {
        int StatisticsId { get; }
        Statistics Statistics { get; }
    
        ulong GuildId { get; }
        Guild Guild { get; }
    }
}
