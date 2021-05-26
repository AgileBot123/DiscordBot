using ModBot.Domain.DTO.ChangelogDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
    public interface IChangelogService
    {
        Task<IChangelog> GetChangeLog(int id);
        Task<IEnumerable<IChangelog>> GetAllChangelogs();
        bool CreateChangelog(ChangeLogDto createChangelog);
        Task<bool> DeleteChangelog(int id);
        Task<bool> UpdateChangelog(ChangeLogDto updateChangelog, int id);
    }
}
