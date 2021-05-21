﻿using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class BannedWord : IBannedWord
    {
        private readonly int id;
        private readonly string word;
        private readonly int strikes;
        private readonly string punishment;

        public int Id => id;
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
    }
}