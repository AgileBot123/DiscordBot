using ModBot.DAL.Repository;
using ModBot.Domain.DTO.BannedWordDto;
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
    public class BannedWordService : IBannedWordService
    {
        private readonly DatabaseRepository _databaseRepository;
        public BannedWordService(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public bool CreateBannedWord(CreateBannedWordDto createBannedWord)
        {
            var createdBannedWord = new BannedWord(
                word: createBannedWord.Word,
                strikes: createBannedWord.Strikes,
                punishment: createBannedWord.Punishment);

            return _databaseRepository.CreateBannedWord(createdBannedWord);
        }

        public async Task<bool> DeleteBannedWord(int id)
        {
            var getBannedWord = await _databaseRepository.GetBannedWord(id);

            if(getBannedWord != null)
            {
                _databaseRepository.DeleteBannedWord(getBannedWord);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<IBannedWord>> GetAllBannedWords()
        {
            var bannedWords = await _databaseRepository.GetAllBannedWords();

            if (bannedWords.Count() == 0)
                return null;

            return bannedWords;
        }

        public async Task<IBannedWord> GetBannedWord(int id)
        {
            var bannedWord = await _databaseRepository.GetBannedWord(id);

            if (bannedWord == null)
                return null;

            return bannedWord;
        }

        public async Task<bool> UpdateBannedWord(UpdateBannedWordDto updatePunishment, int id)
        {
            throw new NotImplementedException();
        }
    }
}
