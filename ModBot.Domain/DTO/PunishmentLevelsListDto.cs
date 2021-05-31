using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO
{
    public class PunishmentLevelsListDto
    {
        public List<PunishmentDto> Punishments { get; set; } = new List<PunishmentDto>();
    }
}
