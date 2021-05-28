using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ModelsInterfaces
{
    public interface IPunishment
    {
        ulong Id { get; }
        int StrikesAmount {get;}
        int TimeOutUntil { get;  }
    }
}
