using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
    public interface ILoggerManager
    {
        void Info(string info, string className);
        void Error(Exception ex, string className);
    }
}
