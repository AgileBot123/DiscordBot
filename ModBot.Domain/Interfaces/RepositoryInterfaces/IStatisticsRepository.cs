using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<IStatistics>> GetAllStatistics();
        Task<IStatistics> GetStatistics(int id);
        bool AddToStatistics(IStatistics stats);
    }
}
