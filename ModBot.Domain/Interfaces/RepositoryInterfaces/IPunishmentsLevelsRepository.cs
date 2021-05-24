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
        bool CreateGetPunishment(IPunishmentsLevels createPunished);
        bool DeleteGetPunishment(IPunishmentsLevels deletePunsihedLevel);
        bool UpdateGetPunishment(IPunishmentsLevels updatePunished, int id);
    }
}
