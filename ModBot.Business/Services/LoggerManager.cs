using ModBot.Domain.Interfaces.ServiceInterface;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Business.Services
{
    public class LoggerManager : ILoggerManager
    {
        private ILogger _logger; 

        public LoggerManager()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger(); 
        }


        public void Error(Exception ex, string className)
        {
            _logger.Error($"{className}: {ex}");
        }

        public void Info(string info, string className)
        {
            _logger.Info($"{className}: {info}");
        }
    }
}
