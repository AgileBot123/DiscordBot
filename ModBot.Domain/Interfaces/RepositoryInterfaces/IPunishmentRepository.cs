using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPunishmentRepository
    {
        Task<Punishment> CreatePunishment();
        List<Punishment> GetAllPunishments();
        Task<List<Punishment>> GetAllPunishmentsAsync();
        Task<bool> UpdatePunishment(Punishment memberPunishment);
    }
}
