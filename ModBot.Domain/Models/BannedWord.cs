using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class BannedWord : IBannedWord
    {
        private readonly string word;
        private readonly int strikes;
        private readonly string punishment;

        public string Word => word;
        public int Strikes => strikes;
        public string Punishment => punishment;

        /// <summary>
        /// Ifall man vill ha konstruktorer med parametrar måste man ha en 
        /// private konstruktor för Entity framework
        /// </summary>
        private BannedWord()
        {
        }
        public BannedWord(int strikes, string punishment)
        {
            
            this.strikes = strikes;
            this.punishment = punishment;


        }
        public BannedWord(string word, int strikes, string punishment)
        {
            this.word = word;
            this.strikes = strikes;
            this.punishment = punishment;


        }
    }
}
