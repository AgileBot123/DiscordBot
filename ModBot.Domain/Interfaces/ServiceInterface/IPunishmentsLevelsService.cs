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
        Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels(ulong guilId);
        bool CreatePunishmentLevel(PunishmentSettingsDto createPunished);
        Task<bool> DeletePunishemntLevel(int id);
        Task<bool> UpdatePunishmentLevel(PunishmentSettingsDto updatePunishment, int id);

    }
}
