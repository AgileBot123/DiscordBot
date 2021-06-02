using ModBot.DAL.Repository;
using ModBot.Domain.DTO.StatisticsDt;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly DatabaseRepository _dataRepo;
        public StatisticsService(DatabaseRepository dataRepo)
        {
            _dataRepo = dataRepo;
        }

        public async Task<bool> RefreshStatisticsInfo(ulong guildId)
        {
            //Siffrorna och strängen ska bytas ut mot fungerande metoder 
            var createStats = new Statistics
                (
                    await NumberOfMembersInNumber(guildId),
                    await NumberOfBannedWords(guildId),
                    await GetAllBannedWordCountPerGuild(guildId),
                    await GetCommandCountPerGuild(guildId),
                    await TotalNumberofStrikes(guildId),
                    await AverageNumberOfStrikes(guildId),
                    3               
                );

            var result = _dataRepo.AddToStatistics(createStats);

            if (result)
                return true;

            return false;
        }

        public async Task<IEnumerable<IStatistics>> GetAllStatistics()
        {
           return await _dataRepo.GetAllStatistics();
        }

        public async Task<IStatistics> GetSpecificStats(int id)
        {
            return await _dataRepo.GetStatistics(id);
        }
        public Task<int> GetCommandCountPerGuild(ulong guildId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAllBannedWordCountPerGuild(ulong guildId)
        {
            var bannedWords = await _dataRepo.GetAllBannedWords();
            var bannedWordGuilds = await _dataRepo.GetAllGuildPunishments();

            var profanity = bannedWordGuilds.Where(x => x.GuildId == guildId).Select(x => x.PunishmentId).ToList();

            var counter = new List<int>();
            foreach (var word in profanity)
            {
                counter.Add(word);
            }
            return counter.Count();
        }


        #region private Methods
        private async Task<int> NumberOfMembersInNumber(ulong guildId)
        {
            var members = await _dataRepo.GetAllMembers();
            return members.Count();
        }

        private async Task<int> NumberOfBannedWords(ulong guildId)
        {
            var BannedWords = await _dataRepo.GetAllBannedWords();
            List<string> newList = new List<String>();
            foreach (var item in BannedWords.Select(x => x.Profanity))
            {
                newList.Add(item);
            }
            return newList.Count();
        }

        private async Task<int> TotalNumberofStrikes(ulong guildId)
        {
            return 0;
        }


        private async Task<double> AverageNumberOfStrikes(ulong guildId)
        {
            return 0;
        }


        #endregion


    }
}
