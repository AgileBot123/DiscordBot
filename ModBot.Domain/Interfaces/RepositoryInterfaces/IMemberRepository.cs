using ModBot.Domain.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IMemberRepository
    {
        Task<IMember> GetMember(ulong id);
        Task<IEnumerable<IMember>> GetAllMembers();
    }
}
