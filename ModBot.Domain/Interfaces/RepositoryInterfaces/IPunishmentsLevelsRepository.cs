using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ServiceInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPunishmentsLevelsRepository
    {
        Task<IPunishmentsLevels> GetPunishmentSetting(int id);
        Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels();
        bool CreatePunishmentSetting(IPunishmentsLevels createPunished);
        bool DeletePunishmentSetting(IPunishmentsLevels deletePunsihedLevel);
        bool UpdatePunishmentSetting(IPunishmentsLevels updatePunished, int id);
    }
}
