using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ModelsInterfaces
{
    public interface IGuildPunishment
    {
        ulong GuildId { get; }
        Guild Guild { get; }
        int PunishmentId { get; }
        Punishment Punishment { get; }
    }
}
