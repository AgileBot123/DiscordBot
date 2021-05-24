using ModBot.DAL.Repository;
using ModBot.Domain.DTO.ChangelogDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class ChangelogService : IChangelogService
    {

        private readonly DatabaseRepository _databaseRepository;
        public ChangelogService(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }


        public void CreateChangelog(CreateChangeLogDto createChangelog)
        {
            throw new NotImplementedException();
        }

        public void DeleteChangelog(IChangelog changelog)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IChangelog>> GetAllChangelogs()
        {
            throw new NotImplementedException();
        }

        public Task<IChangelog> GetChangeLog(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateChangelog(UpdateChangelogDto updateChangelog, int id)
        {
            throw new NotImplementedException();
        }
    }
}
