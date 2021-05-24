using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO
{
    public class UpdatePunishmentLevelDto
    {
        public int TimeOutLevel { get; }
        public int KickLevel { get; }
        public int BanLevel { get; }
        public DateTime SpamMuteTime { get; }
        public DateTime StrikeMuteTime { get; }
    }
}
