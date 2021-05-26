using ModBot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
   
    public class PunishmentsLevels : IPunishmentsLevels
    {
        private int id;
        private readonly int timeOutLevel;
        private readonly int kickLevel;
        private readonly int banLevel;
        private readonly DateTime spamMuteTime;
        private readonly DateTime strikeMuteTime;


        public int Id => id;
        public int TimeOutLevel => timeOutLevel;
        public int KickLevel => kickLevel;
        public int BanLevel => banLevel;
        public DateTime SpamMuteTime => spamMuteTime;
        public DateTime StrikeMuteTime => strikeMuteTime;

        private PunishmentsLevels(){}


        public PunishmentsLevels(int timeoutLevel, int kickLevel, int banLevel, DateTime spamMuteLevel, DateTime strikeMuteLevel)
        {
            this.timeOutLevel = timeoutLevel;
            this.kickLevel = kickLevel;
            this.banLevel = banLevel;
            this.spamMuteTime = spamMuteLevel;
            this.strikeMuteTime = strikeMuteLevel;
        }


        public PunishmentsLevels(int id, int timeoutLevel, int kickLevel, int banLevel, DateTime spamMuteLevel, DateTime strikeMuteLevel)
        {
            this.id = id;
            this.timeOutLevel = timeoutLevel;
            this.kickLevel = kickLevel;
            this.banLevel = banLevel;
            this.spamMuteTime = spamMuteLevel;
            this.strikeMuteTime = strikeMuteLevel;          
        }
    }
}
