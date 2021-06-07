using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO.BannedWordDtos
{
   public class BannedWordListDto
    {
        public List<BannedWordDto> BannedWordList { get; set; } = new List<BannedWordDto>();
        public ulong GuildId { get; set; }
    }
    
}
