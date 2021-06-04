using ModBot.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models
{
    public class SettingsModel
    {
        public string Profanity { get; set; }
        public int Strikes { get; set; }
        public string Punishment { get; set; }
    }
}
