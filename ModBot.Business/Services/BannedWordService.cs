using ModBot.DAL.Repository;
using ModBot.Domain.DTO.BannedWordDto;
using ModBot.Domain.DTO.ChangelogDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class BannedWordService : IBannedWordService
    {
        private readonly DatabaseRepository _databaseRepository;
        public BannedWordService(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }


        public void CreateBannedWord(CreateBannedWordtDto createBannedWord)
        {
            throw new NotImplementedException();
        }

        public void DeleteBannedWord(IBannedWord bannedWord)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IBannedWord>> GetAllBannedWords()
        {
            throw new NotImplementedException();
        }

        public Task<IBannedWord> GetBannedWord(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBannedWord(UpdateBannedWordDto updatePunishment, int id)
        {
            throw new NotImplementedException();
        }
    }
}
