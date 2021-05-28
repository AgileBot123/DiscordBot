using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IMemberPunishmentRepository
    {
        Task<bool> AddToMemberPunishment(ulong memberId, int punishedId);
        Task GetMemberPunishment(ulong memberId, ulong guildId);
        Task<List<MemberPunishment>> GetAllMemberPunishments();
    }
}
