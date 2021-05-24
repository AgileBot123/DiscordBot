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
        Task CreateChangelog(CreateChangeLogDto createChangelog);
        Task DeleteChangelog(IChangelog changelog);
        Task UpdateChangelog(UpdateChangelogDto updateChangelog, int id);
    }
}
