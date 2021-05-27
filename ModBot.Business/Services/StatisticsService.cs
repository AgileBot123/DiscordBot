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

        public async Task<bool> CreateDataToStatistics()
        {

            //Siffrorna och strängen ska bytas ut mot fungerande metoder 
            var createStats = new Statistics
                (
                    await numberOfMembersInNumber(),
                    await numberOfBannedWords(),
                    1,
                    3,
                    3,
                    await totalNumberofStrikes(),
                    await averageNumberOfStrikes(),
                    "!Ping"
                );

            var result = _dataRepo.AddToStatistics(createStats);

            if (true)
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
        private async Task<int> numberOfMembersInNumber()
        {
            var members = await _dataRepo.GetAllMembers();
            return members.Count();
        }

        private async Task<int> numberOfBannedWords()
        {
            var BannedWords = await _dataRepo.GetAllBannedWords();
            List<string> newList = new List<String>();
            foreach (var item in BannedWords.Select(x => x.Word))
            {
                newList.Add(item);
            }
            return newList.Count();
        }

        private async Task<int> totalNumberofStrikes()
        {
            var members = await _dataRepo.GetAllMembers();
            List<int> newList = new List<int>();
            foreach (var strikes in members.Select(x => x.Strikes))
            {
                newList.Add(strikes);
            }
            return newList.Count();
        }


        private async Task<double> averageNumberOfStrikes()
        {
            var members = await _dataRepo.GetAllMembers();
            var statistiscs = new Statistics();

            List<int> newList = new List<int>();
            foreach (var strikes in members.Select(x => x.Strikes))
            {
                newList.Add(strikes);
            }
            var totalStrikes = newList.Count();


           return statistiscs.AverageStrikesPerMember(members.ToList(), totalStrikes);         
        }
       #endregion


    }
}
