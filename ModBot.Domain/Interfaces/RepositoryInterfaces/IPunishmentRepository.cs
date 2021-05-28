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
        Task<List<Punishment>> GetAllPunishments();
    }
}
