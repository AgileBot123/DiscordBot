using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot.Domain.Models
{
    public class Statistics : IStatistics
    {
        #region Properties   
        private readonly int _id;
        private readonly int _numberOfMember;
        private readonly int _numberOfBannedWords;
        private readonly int _totalStrikesInDatabase;
        private readonly double _averageNumberOfStrikes;
        private readonly double  _medianNumberOfStrikes;
        private readonly int _numberOfTimesBannedWordBeenUsed;
        private int _numberOfTimesEachCommandoBeenUsed;
        private readonly ulong _guildId;

        public Statistics() { }

        public Statistics(int id, int numberOfMember, int numberOfBannedWords, int numberOfTimesBannedWordBeenUsed, int numberOfTimesEachCommandoBeenUsed, int totalStrikesInDatabase, double averageNumberOfStrikes, double medianNumberOfStrikes)
        {
            _id = id;
            _numberOfMember = numberOfMember;
            _numberOfBannedWords = numberOfBannedWords;
            _numberOfTimesBannedWordBeenUsed = numberOfTimesBannedWordBeenUsed;
            _numberOfTimesEachCommandoBeenUsed = numberOfTimesEachCommandoBeenUsed;
            _totalStrikesInDatabase = totalStrikesInDatabase;
            _averageNumberOfStrikes = averageNumberOfStrikes;
            _medianNumberOfStrikes = medianNumberOfStrikes;
        }

        public Statistics(int numberOfMember, int numberOfBannedWords, int numberOfTimesBannedWordBeenUsed, int numberOfTimesEachCommandoBeenUsed, int totalStrikesInDatabase, double averageNumberOfStrikes, double medianNumberOfStrikes)
        {
            _numberOfMember = numberOfMember;
            _numberOfBannedWords = numberOfBannedWords;
            _totalStrikesInDatabase = totalStrikesInDatabase;
            _numberOfTimesBannedWordBeenUsed = numberOfTimesBannedWordBeenUsed;
            _numberOfTimesEachCommandoBeenUsed = numberOfTimesEachCommandoBeenUsed;
            _averageNumberOfStrikes = averageNumberOfStrikes;
            _medianNumberOfStrikes = medianNumberOfStrikes;
        }

        #region Properties
        public int Id
        {
            get { return _id; }
            private set { }
        }


        public int NumberOfMembers
        {
            get { return _numberOfMember; }
            private set { }
        }

        public int NumberOfBannedWords
        {
            get { return _numberOfBannedWords; }
            private set { }
        }
        public int TotalStrikesInDatabase
        {
            get { return _totalStrikesInDatabase; }
            private set { }
        }
        public double AverageNumberOfStrikes
        {
            get { return _averageNumberOfStrikes; }
            private set { }
        }

        public double MedianNumberOfStrikes
        {
            get { return _medianNumberOfStrikes; }
            private set { }
        }

        public ulong GuildId
        {
            get { return _guildId; }
            private set { }
        }

        public Guild Guild { get; private set; }

        public int NumberOfTimesBannedWordBeenUsed
        {
            get { return _numberOfTimesBannedWordBeenUsed; }
            private set { }
        }
        public int NumberOfTimesEachCommandoBeenUsed
        {
            get { return _numberOfTimesEachCommandoBeenUsed; }
            private set { }
        }
        #endregion




        #endregion

        #region Methods
        public double AverageStrikesPerMember(List<IMember> members, int totalStrikesInDatabase)
        {
            return AverageNumberOfStrikes = members.Count() / totalStrikesInDatabase;
        }

        public double MedianStrikePerMember(List<IMember> members)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
