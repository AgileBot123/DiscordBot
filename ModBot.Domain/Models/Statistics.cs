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
        private readonly int _numberOfMembersBeenTimedOut;
        private readonly int _numberOfMembersBeingBanned;
        private readonly int _totalStrikesInDatabase;
        private readonly double _averageNumberOfStrikes;
        private readonly double  _medianNumberOfStrikes;
        private readonly string _mostUsedCommand;

        public Statistics()
        {

        }

        public Statistics(int id, int numberOfMember, int numberOfBannedWords, int numberOfMembersBeenTimedOut, int numberOfMembersBeingBanned, int totalStrikesInDatabase, double averageNumberOfStrikes, double medianNumberOfStrikes, string mostUsedCommand)
        {
            _id = id;
            _numberOfMember = numberOfMember;
            _numberOfBannedWords = numberOfBannedWords;
            _numberOfMembersBeenTimedOut = numberOfMembersBeenTimedOut;
            _numberOfMembersBeingBanned = numberOfMembersBeingBanned;
            _totalStrikesInDatabase = totalStrikesInDatabase;
            _averageNumberOfStrikes = averageNumberOfStrikes;
            _medianNumberOfStrikes = medianNumberOfStrikes;
            _mostUsedCommand = mostUsedCommand;
        }

        public Statistics(int numberOfMember, int numberOfBannedWords, int numberOfMembersBeenTimedOut, int numberOfMembersBeingBanned, int totalStrikesInDatabase, double averageNumberOfStrikes, double medianNumberOfStrikes, string mostUsedCommand)
        {
            _numberOfMember = numberOfMember;
            _numberOfBannedWords = numberOfBannedWords;
            _numberOfMembersBeenTimedOut = numberOfMembersBeenTimedOut;
            _numberOfMembersBeingBanned = numberOfMembersBeingBanned;
            _totalStrikesInDatabase = totalStrikesInDatabase;
            _averageNumberOfStrikes = averageNumberOfStrikes;
            _medianNumberOfStrikes = medianNumberOfStrikes;
            _mostUsedCommand = mostUsedCommand;
        }


        public int Id
        {
            get { return _id; }
            private set { }
        }

        public string MostUsedCommand
        {
            get { return _mostUsedCommand; }
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

        public int NumberOfMembersBeenTimedOut
        {
            get { return _numberOfMembersBeenTimedOut; }
            private set { }
        }

        public int NumberOfMembersBeingBanned
        {
            get { return _numberOfMembersBeingBanned; }
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





        #endregion

        #region Methods
        public double AverageStrikesPerMember(List<IMember> members, int totalStrikesInDatabase)
        {
            return AverageNumberOfStrikes = members.Count() / totalStrikesInDatabase;
        }

        //Correct or not
        public double MedianStrikePerMember(List<IMember> members)
        {
            var sum = members.OrderBy(x => x.Strikes).ToList();
            return sum.Count() / 2;
        }
        #endregion
    }
}
