using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class AntiSpamModel
    {
        public SocketGuildUser User { get; set; }
        public int Counter { get; set; }
        public DateTimeOffset Timer { get; set; }
        public string TempMessage { get; set; }
    }
}
