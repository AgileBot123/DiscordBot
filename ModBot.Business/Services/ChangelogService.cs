using ModBot.DAL.Repository;
using ModBot.Domain.DTO.ChangelogDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool CreateChangelog(ChangeLogDto createChangelog)
        {
            var createdLog = new Changelog(
                changeDate: createChangelog.ChangeDate,
                changed: createChangelog.Changed);

            return _databaseRepository.CreateChangelog(createdLog);
        }

        public async Task<bool> DeleteChangelog(int id)
        {
            var getLog = await _databaseRepository.GetChangelog(id);

            if(getLog != null)
            {
                _databaseRepository.DeleteChangelog(getLog);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<IChangelog>> GetAllChangelogs()
        {
            var logs = await _databaseRepository.GetAllChangelogs();

            if (logs.Count() == 0)
                return null;

            return logs;
        }

        public async Task<IChangelog> GetChangeLog(int id)
        {
            var log = await _databaseRepository.GetChangelog(id);

            if (log == null)
                return null;

            return log;
        }

        public async Task<bool> UpdateChangelog(ChangeLogDto updateChangelog, int id)
        {
            var selectlog = await _databaseRepository.GetChangelog(id);

            if( selectlog != null)
            {
                var newlog = new Changelog(updateChangelog.ChangeDate,
                                            updateChangelog.Changed);

                var result = _databaseRepository.UpdateChangelog(id,newlog);

                if (result)
                    return true;
              
            }
            return false;

        }
    }
}
