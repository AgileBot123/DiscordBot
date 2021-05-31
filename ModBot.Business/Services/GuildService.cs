using ModBot.DAL.Repository;
using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class GuildService : IGuildService
    {
        private readonly DatabaseRepository _databaseRepository;

        public GuildService(DatabaseRepository databaseRepository)
        {
            this._databaseRepository = databaseRepository;
        }
        public async Task<IEnumerable<IGuild>> GetAllGuilds()
        {
            var guilds = await _databaseRepository.GetAllGuilds();
            return guilds;
        }

        public async Task<IGuild> GetGuildById(ulong guildId)
        {
            var guild = await _databaseRepository.GetGuild(guildId);

            return guild;
        }

        public async Task<bool> CreateGuild(IGuild guild)
        {
            return await _databaseRepository.CreateGuild(guild);
        }
    }
}
