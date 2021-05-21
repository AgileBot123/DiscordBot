using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class PunishmentsLevelsService : IPunishmentsLevelsService
    {

     
        public PunishmentsLevelsService()
        {
            
        }

        public Task<PunishmentsLevels> GetPunishedLevel(int id)
        {
            throw new NotImplementedException();
        }
    }
}
