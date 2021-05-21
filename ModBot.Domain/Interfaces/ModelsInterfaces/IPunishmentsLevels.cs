using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces
{
    public interface IPunishmentsLevels
    {
        int TimeOut { get; set; }
        int Kick { get; set; }
        int Ban { get; set; }
    }
}
