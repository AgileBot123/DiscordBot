using ModBot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
   
    public class PunishmentsLevel : IPunishmentsLevels
    {
        private readonly int _id;
        private readonly int _timeOutLevel;
        private readonly int _kickLevel;
        private readonly int _banLevel;
        private readonly DateTime _spamMuteTime;
        private readonly DateTime _strikeMuteTime;


        public int Id
        {
            get { return _id; }
            private set { }
        }
        public int TimeOutLevel
        {
            get { return _timeOutLevel; }
            private set { }
        }
        public int KickLevel
        {
            get { return _kickLevel; }
            private set { }
        }
        public int BanLevel
        {
            get { return _banLevel; }
            private set { }
        }
        public DateTime SpamMuteTime
        {
            get { return _spamMuteTime; }
            private set { }
        }
        public DateTime StrikeMuteTime
        {
            get { return _strikeMuteTime; }
            private set { }
        }

        private PunishmentsLevel(){}


        public PunishmentsLevel(int timeoutLevel, int kickLevel, int banLevel, DateTime spamMuteLevel, DateTime strikeMuteLevel)
        {
            this._timeOutLevel = timeoutLevel;
            this._kickLevel = kickLevel;
            this._banLevel = banLevel;
            this._spamMuteTime = spamMuteLevel;
            this._strikeMuteTime = strikeMuteLevel;
        }


        public PunishmentsLevel(int id, int timeoutLevel, int kickLevel, int banLevel, DateTime spamMuteLevel, DateTime strikeMuteLevel)
        {
            this._id = id;
            this._timeOutLevel = timeoutLevel;
            this._kickLevel = kickLevel;
            this._banLevel = banLevel;
            this._spamMuteTime = spamMuteLevel;
            this._strikeMuteTime = strikeMuteLevel;          
        }
    }
}
