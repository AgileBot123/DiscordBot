using ModBot.Domain.Interfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Testing.Mocks.ControllerMocks
{
    public static class PunishedLevelControllerMock 
    {
        public static List<IPunishmentsLevels> ListofPunishedLevelMock = new List<IPunishmentsLevels>
        {
            new PunishmentsLevels(1, 1, 2, 3, default(DateTime), default(DateTime))
        };
    }
}
