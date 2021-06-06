using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO.BannedWordDtos
{
    public class BannedWordForFileDto
    {
        public string Profanity { get; set; }
        public ulong GuildId { get; set; }
        public int Strikes { get; set; }
        public string Punishment { get; set; }
    }
}
