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
        void CreateChangelog(IChangelog createLog);
        void DeleteChangelog(IChangelog changelog);
        void UpdateChangelog(int id, IChangelog changelog);
    }
}
