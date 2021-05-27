using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models
{
    public class BannedWordModel
    {
        public string Word { get; set; }
        public int Strikes { get; set; }
        public string Punishment { get; set; }
    }
}
