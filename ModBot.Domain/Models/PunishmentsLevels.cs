using ModBot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class PunishmentsLevels : IPunishmentsLevels
    {
        private int id;
        private readonly int timeoutLevel;
        private readonly int kickLevel;
        private readonly int banLevel;
        private readonly DateTime spamMuteLevel;
        private readonly DateTime strikeMuteLevel;


        public int Id => id;
        public int TimeOutLevel => timeoutLevel;
        public int KickLevel => kickLevel;
        public int BanLevel => banLevel;
        public DateTime SpamMuteTime => spamMuteLevel;
        public DateTime StrikeMuteTime => strikeMuteLevel;

        private PunishmentsLevels(){}


        public PunishmentsLevels(int id, int timeoutLevel, int kickLevel, int banLevel, DateTime spamMuteLevel, DateTime strikeMuteLevel)
        {
            this.id = id;
            this.timeoutLevel = timeoutLevel;
            this.kickLevel = kickLevel;
            this.banLevel = banLevel;
            this.spamMuteLevel = spamMuteLevel;
            this.strikeMuteLevel = strikeMuteLevel;          
        }
    }
}
