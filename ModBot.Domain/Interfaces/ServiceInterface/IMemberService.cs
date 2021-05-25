using ModBot.Domain.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
    public interface IMemberService
    {
        Task<IMember> GetMemberById(ulong id);
        Task<IEnumerable<IMember>> GetAllMembers();
    }
}
