using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModBot.Domain.Models
{
    public class BannedWordGuilds : IBannedWordGuilds
    {
        #region Properties
        private ulong _guildId;
        private string _profanity;

        [Key]
        public ulong GuildId
        {
            get { return _guildId; }
            private set { }
        }

        public Guild Guild { get; private set; }

        [Key]
        public string BannedWordProfanity
        {
            get { return _profanity; }
            private set { }
        }
        public BannedWord BannedWord { get; private set; }
        #endregion


        #region Constructors
        public BannedWordGuilds() {}

        public BannedWordGuilds(ulong guildId, string bannedWordWord)
        {
            _guildId = guildId;
            _profanity = bannedWordWord;
        }
        #endregion

    }
}
