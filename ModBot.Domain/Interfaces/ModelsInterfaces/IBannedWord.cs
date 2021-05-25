using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ModelsInterfaces
{
    public interface IBannedWord
    {
        string Word { get;}
        int Strikes { get; }
        string Punishment { get; }
    }
}
