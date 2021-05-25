using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO
{
    public class UpdatePunishmentLevelDto
    {
        public int TimeOutLevel { get; set; }
        public int KickLevel { get; set; }
        public int BanLevel { get; set;  }
        public DateTime SpamMuteTime { get; set; }
        public DateTime StrikeMuteTime { get; set; }
    }
}
