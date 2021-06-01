﻿using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IBannedWordGuildRepository
    {
        Task<List<BannedWordGuilds>> GetAllBannedWordGuild(); 
    }
}
