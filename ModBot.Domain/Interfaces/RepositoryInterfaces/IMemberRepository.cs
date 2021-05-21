using ModBot.Domain.interfaces;
using System.Collections.Generic;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IMemberRepository
    {
        IMember Get(ulong id);
        IEnumerable<IMember> GetAll();
    }
}
