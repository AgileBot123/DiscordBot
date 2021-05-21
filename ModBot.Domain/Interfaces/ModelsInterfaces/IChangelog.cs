using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ModelsInterfaces
{
    public interface IChangelog
    {
        int Id { get;  }
        DateTime ChangedDate { get; }
        string Changed { get; }
    }
}
