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

        public BannedWord(){}
        public BannedWord(string word, int strikes, string punishment)
        {
            this._profanity = word;
            this._strikes = strikes;
            this._punishment = punishment;
        }

        public BannedWord(int strikes, string punishment, int bannedWordUsedCount)
        {

            this._strikes = strikes;
            this._punishment = punishment;
            _bannedWordUsedCount = bannedWordUsedCount;
        }
        public BannedWord(string word, int strikes, string punishment, int bannedWordUsedCount)
        {
            this._profanity = word;
            this._strikes = strikes;
            this._punishment = punishment;
            _bannedWordUsedCount = bannedWordUsedCount;
        }
    }
}
