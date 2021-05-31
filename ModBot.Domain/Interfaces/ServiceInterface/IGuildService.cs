using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
    public interface IGuildService
    {
        Task<IGuild> GetGuildById(ulong guildId);
        Task<IEnumerable<IGuild>> GetAllGuilds();
        Task<bool> CreateGuild(IGuild guild);
    }
}
