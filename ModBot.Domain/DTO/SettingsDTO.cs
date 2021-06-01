using ModBot.Domain.DTO.BannedWordDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO
{
    public class SettingsDTO
    {
        //BannedWord
        public List<BannedWordDto> BannedWordList { get; set; } = new List<BannedWordDto>();

        //Punishment
        public int TimeOutLevel { get; set; }
        public int KickLevel { get; set; }
        public int BanLevel { get; set; }
        public int SpamMuteTime { get; set; }
        public int StrikeMuteTime { get; set; }
    }
}
