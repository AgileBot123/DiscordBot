using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO
{
    public class GuildDto
    {
        public ulong Id { get; set; }
        public bool HasBot { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
    }
}
