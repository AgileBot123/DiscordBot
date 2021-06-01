using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO
{
    public class SettingsDTO
    {
        //BannedWord
        public string Profanity { get; set; }
        public int Strikes { get; set; }
        public string Punishment { get; set; }
        public int BannedWordUsedCount { get; set; }

        //Punishment
        public int TimeOutLevel { get; set; }
        public int KickLevel { get; set; }
        public int BanLevel { get; set; }
        public int SpamMuteTime { get; set; }
        public int StrikeMuteTime { get; set; }
    }
}
