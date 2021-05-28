using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO
{
    public class PunishmentDto
    {
        public int TimeOutLevel { get; set; }
        public int KickLevel { get; set; }
        public int BanLevel { get; set; }
        public int SpamMuteTime { get; set; }
        public int StrikeMuteTime { get; set; }
    }
}
