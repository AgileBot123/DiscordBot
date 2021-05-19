using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class PunishedLevelService : IPunishedLevelService
    {

     
        public PunishedLevelService()
        {
            
        }

        public Task<PunishedLevel> GetPunishedLevel(int id)
        {
            throw new NotImplementedException();
        }
    }
}
