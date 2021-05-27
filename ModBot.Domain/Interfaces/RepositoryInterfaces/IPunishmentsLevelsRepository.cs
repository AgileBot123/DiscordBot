using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ServiceInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPunishmentsLevelsRepository
    {
        Task<IPunishmentsLevels> GetPunishment(int id);
        Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels();
        bool CreatePunishment(IPunishmentsLevels createPunished);
        bool DeletePunishment(IPunishmentsLevels deletePunsihedLevel);
        bool UpdatePunishment(IPunishmentsLevels updatePunished, int id);
    }
}
