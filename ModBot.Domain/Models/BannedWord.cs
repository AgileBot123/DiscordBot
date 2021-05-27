using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class BannedWord : IBannedWord
    {
        private readonly string _word;
        private readonly int _strikes;
        private readonly string _punishment;

        public string Word
        {
            get { return _word; }
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

        public BannedWord(){}

        public BannedWord(int strikes, string punishment)
        {
            
            this._strikes = strikes;
            this._punishment = punishment;
        }
        public BannedWord(string word, int strikes, string punishment)
        {
            this._word = word;
            this._strikes = strikes;
            this._punishment = punishment;
        }
    }
}
