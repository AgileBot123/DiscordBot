using ModBot.Domain.DTO;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
   public interface IPunishmentsLevelsService
    {
        Task<IPunishmentsLevels> GetPunishmentLevel(int id);
        Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels();
        Task CreatePunishmentLevel(CreatePunishmentDto createPunished);
        Task DeletePunishemntLevel(IPunishmentsLevels punishments);
        Task UpdatePunishmentLevel(UpdatePunishmentLevelDto updatePunishment, int id);

    }
}
