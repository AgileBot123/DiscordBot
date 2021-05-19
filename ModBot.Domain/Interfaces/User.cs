﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.interfaces
{
    public interface User
    {
        ulong Id { get; set; }
        int Strikes { get; set; }
        void AddStrikes(int strikes);
        void RemoveStrikes(int strikes);
    }
}
