using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IMemberPunishment
    {
        Task<bool> AddToMemberPunishment(ulong memberId, int punishedId);
    }
}
