using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models
{
    public class PunishmentModel
    {
        public int TimeOutLevel { get; set; }
        public int KickLevel { get; set; }
        public int BanLevel { get; set; }
        public DateTime SpamMuteTime { get; set; }
        public DateTime StrikeMuteTime { get; set; }
    }
}
