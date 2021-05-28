using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ModelsInterfaces
{
    public interface IGuild
    {
        ulong Id { get; }
        bool HasBot { get;  }
        string Avatar { get; }
        string GuildName { get; }
    }
}
