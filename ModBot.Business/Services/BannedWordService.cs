﻿using ModBot.DAL.Repository;
using ModBot.Domain.DTO.BannedWordDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class BannedWordService : IBannedWordService
    {
        private readonly DatabaseRepository _databaseRepository;
        private static readonly NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        public BannedWordService(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public bool CreateBannedWord(BannedWordDto createBannedWord)
        {
            var createdBannedWord = new BannedWord(
                word: createBannedWord.Profanity,
                strikes: createBannedWord.Strikes,
                punishment: createBannedWord.Punishment
                , createBannedWord.GuildId);

            return _databaseRepository.CreateBannedWord(createdBannedWord);
        }

        public async Task<bool> DeleteBannedWord(ulong guildId, string word)
        {
            var getBannedWord = await GetBannedWord(guildId, word);

            if (getBannedWord != null)
            {
                _databaseRepository.DeleteBannedWord(getBannedWord);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<IBannedWord>> GetAllBannedWords(ulong guildId)
        {
            var allBannedWords = await _databaseRepository.GetAllBannedWords();

            var getAllBannedWordGuilds = allBannedWords.Where(x => x.GuildId == guildId).ToList();

            return getAllBannedWordGuilds;
        }

        public async Task<IBannedWord> GetBannedWord(ulong guildId, string word) =>
                    await _databaseRepository.GetBannedWord(guildId, word);
        

        public async Task<bool> UpdateBannedWordList(BannedWordListDto updatedBannedWordListDto)
        {
            try
            {
                var bannedWordList = await _databaseRepository.GetAllBannedWords();
                var updatedBannedWordList = updatedBannedWordListDto.BannedWordList;
                BannedWord changedBannedWord = null;

                foreach (var updatedBannedWord in updatedBannedWordList)
                {
                    if (bannedWordList.Any(b => b.Profanity.Equals(updatedBannedWord.Profanity)))
                    {
                        changedBannedWord = new BannedWord(                                                   
                                                     updatedBannedWord.Profanity,
                                                     updatedBannedWord.Strikes,
                                                     updatedBannedWord.Punishment,
                                                     updatedBannedWord.GuildId);

                        _databaseRepository.UpdateBannedWord(changedBannedWord);
                    }
                    else
                    {
                        changedBannedWord = new BannedWord(updatedBannedWord.Profanity,
                                                     updatedBannedWord.Strikes,
                                                     updatedBannedWord.Punishment, 
                                                     updatedBannedWord.GuildId);

                        _databaseRepository.CreateBannedWord(changedBannedWord);
                    }
                }

                foreach (var bannedWord in bannedWordList)
                {
                    if (!updatedBannedWordList.Any(b => b.Profanity.Equals(bannedWord.Profanity)))
                    {
                        changedBannedWord = new BannedWord(bannedWord.Profanity,
                                                     bannedWord.Strikes,
                                                     bannedWord.Punishment,
                                                     bannedWord.GuildId);

                       _databaseRepository.DeleteBannedWord(changedBannedWord);
                    }
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
