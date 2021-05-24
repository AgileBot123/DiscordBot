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
        bool CreateChangelog(CreateChangeLogDto createChangelog);
        Task<bool> DeleteChangelog(int id);
        Task<bool> UpdateChangelog(UpdateChangelogDto updateChangelog, int id);
    }
}
