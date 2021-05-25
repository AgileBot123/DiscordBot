using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models
{
    public class DiscordServer
    {
        private readonly ulong id;
        private readonly string name;
        private readonly string icon;
        private readonly bool hasbot;
        public ulong Id => id;
        public string Name => name;
        public string Icon => icon;
        public bool HasBot => hasbot; //add to DiscordServer

        public DiscordServer(ulong id, string name, string icon)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
        }
        public DiscordServer(ulong id, string name, string icon, bool hasbot)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.hasbot = hasbot;
        }
    }
}
