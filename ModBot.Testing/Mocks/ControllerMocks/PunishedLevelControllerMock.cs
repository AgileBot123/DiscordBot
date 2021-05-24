using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Testing.Mocks.ControllerMocks
{
    public class PunishedLevelControllerMock 
    {
        private List<PunishmentsLevels> punishmentsLevelsTimeout = new List<PunishmentsLevels>
        {
            new PunishmentsLevels(1, 1, 2, 3, default(DateTime), default(DateTime))
        };
    }
}
