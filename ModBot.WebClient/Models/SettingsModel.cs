using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models
{
    public class SettingsModel
    {
        public int ID { get; set;  }
        public string Insert_Word { get; set; }
        public string BadWord1 { get; set; }
        public string BadWord2 { get; set; }
        public string BadWord3 { get; set; }
        public string Bad_Sentence1 { get; set; }
        public string Bad_Sentence2 { get; set; }


    }
}
