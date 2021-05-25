using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface ICommandLogicRepository
    {
        Member GetUser(ulong id);
    }
}
