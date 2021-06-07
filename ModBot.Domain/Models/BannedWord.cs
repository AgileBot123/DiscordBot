using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class BannedWord : IBannedWord
    {
        private readonly string _profanity;
        private readonly int _strikes;
        private readonly string _punishment;
        private int _bannedWordUsedCount;
        private ulong _guildId;

        public string Profanity
        {
            get { return _profanity; }
            private set { }
        }
        public int Strikes
        {
            get { return _strikes; }
            private set { }
        }
        public string Punishment
        {
            get { return _punishment; }
            private set { }
        }

        public int BannedWordUsedCount
        {
            get { return _bannedWordUsedCount; }
            private set { }
        }

        public ulong GuildId
        {
            get { return _guildId; }
            private set { }
        }

        public Guild Guild { get; private set; }


        public BannedWord(){}


        public BannedWord(string word, ulong guildId)
        {
            this._profanity = word;
            this._guildId = guildId;
        }


        public BannedWord(string word, string punishment, int strikes)
        {
            this._profanity = word;
            this._punishment = punishment;
            this._strikes = strikes;
        }

        public BannedWord(string word, int strikes, string punishment, ulong guildId)
        {
            this._profanity = word;
            this._strikes = strikes;
            this._punishment = punishment;
            _guildId = guildId;
        }

        public BannedWord(int strikes, string punishment, int bannedWordUsedCount, ulong guildId)
        {

            this._strikes = strikes;
            this._punishment = punishment;
            _bannedWordUsedCount = bannedWordUsedCount;
            _guildId = guildId;
        }
        public BannedWord(string word, int strikes, string punishment, int bannedWordUsedCount, ulong guildId)
        {
            this._profanity = word;
            this._strikes = strikes;
            this._punishment = punishment;
            _bannedWordUsedCount = bannedWordUsedCount;
            _guildId = guildId;
        }
    }
}
