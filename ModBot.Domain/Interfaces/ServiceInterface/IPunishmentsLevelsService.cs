﻿using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
   public interface IPunishmentsLevelsService
    {
         Task<PunishmentsLevels> GetPunishmentLevel(int id);
         Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels();
    }
}
