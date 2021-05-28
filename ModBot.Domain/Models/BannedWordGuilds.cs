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
        private string _profanity;

        public ulong GuildId
        {
            get { return _guildId; }
            private set { }
        }

        public Guild Guild { get; private set; }

        public string BannedWordProfanity
        {
            get { return _profanity; }
            private set { }
        }
        public BannedWord BannedWord { get; private set; }
        #endregion


        #region Constructors
        private BannedWordGuilds() {}

        public BannedWordGuilds(ulong guildId, string bannedWordWord)
        {
            _guildId = guildId;
            _profanity = bannedWordWord;
        }
        #endregion

    }
}
