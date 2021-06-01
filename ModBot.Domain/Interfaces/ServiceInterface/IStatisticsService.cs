using ModBot.Domain.interfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
   public interface IStatisticsService
    {
        Task<IEnumerable<IStatistics>> GetAllStatistics();
        Task<IStatistics> GetSpecificStats(int id);
        Task<bool> RefreshStatisticsInfo(ulong guildId);
        Task<int> GetCommandCountPerGuild(ulong guildId);
        Task<int> GetAllBannedWordCountPerGuild(ulong guildId);
    }
}
