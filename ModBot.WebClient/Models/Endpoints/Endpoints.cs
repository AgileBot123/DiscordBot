using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models.Endpoints
{
    public class Endpoints : IEndpoints
    {
        
        public string CreateBannedWord => Base+"createbannedword";

        public string GetGuild => Base + "getguild";

        public string GetBannedWord => Base+"getbannedword";

        public string GetAllBannedWords => Base + "getallbannedwords";

        public string DeleteBannedWord => Base + "deletebannedword";

        public string UpdateBannedWordList => Base + "updatebannedwordlist";

        public string CreateLog => Base + "/createlog";

        public string GetChangeLog => Base + "getchangelog";

        public string GetAllLogs => Base + "getalllogs";

        public string DeleteLog => Base + "deletelog";

        public string UpdateLog => Base + "udpatelog";

        public string CreatePunishedLevel => Base + "createpunishmentlevel";

        public string GetPunishedLevel => Base + "getpunishedlevel";

        public string GetPunishedLevels => Base + "getpunishedlevels";

        public string DeletePunishedLevel => Base + "deletepunishedlevel";

        public string UpdatePunishedLevel => Base + "updatepunishedlevel";

        public string GetMember => Base + "getmember";

        public string GetAllMembers => Base + "getallmembers";

        public string Host => "https://localhost:44396/";
        public string Base => Host + "api/";
    }
}
