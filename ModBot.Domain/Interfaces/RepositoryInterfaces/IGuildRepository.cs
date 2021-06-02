using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IGuildRepository
    {
        Task<IGuild> GetGuild(ulong GuildId);
        Task<IEnumerable<IGuild>> GetAllGuilds();
        bool CreateGuild(IGuild guild);
        bool UpdateGuild(IGuild guild);
    }
}
