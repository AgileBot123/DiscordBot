using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models
{
    public class GuildModel
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool HasBot { get; set; }

        public GuildModel() {}

        public GuildModel(ulong id, string name, string icon, bool hasbot)
        {
            this.Id = id;
            this.Name = name;
            this.Icon = icon;
            this.HasBot = hasbot;
        }
    }
}
