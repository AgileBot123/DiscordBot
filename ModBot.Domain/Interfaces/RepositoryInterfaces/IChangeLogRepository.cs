using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IChangeLogRepository
    {
        Task<IChangelog> GetChangelog(int id);
        Task<IEnumerable<IChangelog>> GetAllChangelogs();
        bool CreateChangelog(IChangelog createLog);
        bool DeleteChangelog(IChangelog changelog);
        bool UpdateChangelog(int id, IChangelog changelog);
    }
}
