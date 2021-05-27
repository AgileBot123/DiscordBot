using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Extensions.Routes
{
    public static class Routes
    {
        public const string Root = "api";
        private const string Base = Root + "/";

        public class BannedWords
        {
            public const string GetBannedWord = Base + "GetBannedWord/{id}";
            public const string GetAllBannedWords = Base + "GetAllBannedWords";
            public const string CreateBannedWord = Base + "CreateBannedWord";
            public const string DeleteBannedWord = Base + "DeleteBannedWord";
            public const string UpdateBannedWord = Base + "UpdateBannedWord";

        }
        public class ChangeLog
        {
            public const string GetLog = Base + "GetLog/{id}";
            public const string GetAllLogs = Base + "GetAllLogs";
            public const string CreateLog = Base + "CreateLog";
            public const string UpdateLog = Base + "Updatelog";
            public const string DeleteLog = Base + "DeleteLog";
          
        }
        public class PunishedLevels
        {
            public const string GetPunishedLevel = Base + "GetPunishedLevel/{id}";
            public const string GetPunishedLevels = Base + "GetPunishedLevels";
            public const string CreatePunishedLevel = Base + "CreatePunishedLevel";
            public const string DeletePunishedLevel = Base + "DeletePunishedLevel";
            public const string UpdatePunishedLevel = Base + "UpdatePunishedLevel";
        }
        public class Members
        {
            public const string GetMember = Base + "GetMember";
            public const string GetMembers = Base + "GetMembers";
        }

        public class Statistisc
        {
            public const string GetAllStats = Base + "GetAllStats";
            public const string GetSpecificStats = Base + "GetStat/{id}";
            public const string CreateStatistic = Base + "CreateStats";
        }
    }
}
