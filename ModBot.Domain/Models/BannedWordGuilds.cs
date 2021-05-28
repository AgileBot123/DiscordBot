using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class BannedWordGuilds : IBannedWordGuilds
    {
        #region Properties
        private ulong _guildId;
        private string _bannedWordWord;

        public ulong GuildId
        {
            get { return _guildId; }
            private set { }
        }

        public Guild Guild { get; private set; }

        public string BannedWordWord
        {
            get { return _bannedWordWord; }
            private set { }
        }
        public BannedWord BannedWord { get; private set; }
        #endregion


        #region Constructors
        private BannedWordGuilds() {}

        public BannedWordGuilds(ulong guildId, string bannedWordWord)
        {
            _guildId = guildId;
            _bannedWordWord = bannedWordWord;
        }
        #endregion

    }
}
