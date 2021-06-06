using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.interfaces
{
    public interface IMember
    {
        ulong Id { get;}
        string Avatar { get; }
        string Username { get; }
        bool IsBot { get; }
    }
}
