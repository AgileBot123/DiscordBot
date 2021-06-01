using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO
{
    public class PunishmentLevelsListDto
    {
        public List<PunishmentSettingsDto> Punishments { get; set; } = new List<PunishmentSettingsDto>();
    }
}
