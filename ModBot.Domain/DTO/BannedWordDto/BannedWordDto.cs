﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO.BannedWordDto
{
    public class BannedWordDto
    {
        public string Word { get; set; }
        public int Strikes { get; set; }
        public string Punishment { get; set; }
        
    }
    
}