using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.interfaces
{
    public interface IMember
    {
        ulong Id { get; }
        int Strikes { get;}
        void AddStrikes(int strikes);
        void RemoveStrikes(int strikes);
    }
}
