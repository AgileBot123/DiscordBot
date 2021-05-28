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

        public async Task<bool> RefreshStatisticsInfo()
        {

            //Siffrorna och strängen ska bytas ut mot fungerande metoder 
            var createStats = new Statistics
                (
                    await NumberOfMembersInNumber(),
                    await NumberOfBannedWords(),
                    1,
                    3,
                    3,
                    await TotalNumberofStrikes(),
                    await AverageNumberOfStrikes(),
                    "!Ping"
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


       #region private Methods
        private async Task<int> NumberOfMembersInNumber()
        {
            var members = await _dataRepo.GetAllMembers();
            return members.Count();
        }

        private async Task<int> NumberOfBannedWords()
        {
            var BannedWords = await _dataRepo.GetAllBannedWords();
            List<string> newList = new List<String>();
            foreach (var item in BannedWords.Select(x => x.Word))
            {
                newList.Add(item);
            }
            return newList.Count();
        }

        private async Task<int> TotalNumberofStrikes()
        {
            return 0;
        }


        private async Task<double> AverageNumberOfStrikes()
        {
            return 0;
        }
       #endregion


    }
}
