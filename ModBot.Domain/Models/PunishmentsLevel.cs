using ModBot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
   
    public class PunishmentsLevel : IPunishmentsLevels
    {
        private int _id;
        private  int _timeOutLevel;
        private  int _kickLevel;
        private  int _banLevel;
        private  DateTime _spamMuteTime;
        private  DateTime _strikeMuteTime;


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
