using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
   public interface IPunishedLevelService
    {
        Task<PunishmentsLevels> GetPunishedLevel(int id);

    }
}
