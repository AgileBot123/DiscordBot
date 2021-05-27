using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models.Endpoints
{
    public class Endpoints : IEndpoints
    {
        
        public string CreateBannedWord => "http://localhost:63583/api/createbannedword";

        public string GetBannedWord => "http://localhost:63583/api/getbannedword";

        public string GetAllBannedWords => "http://localhost:63583/api/getallbannedwords";

        public string DeleteBannedWord => "http://localhost:63583/api/deletebannedword";

        public string UpdateBannedWordList => "http://localhost:63583/api/updatebannedwordlist";

        public string CreateLog => "http://localhost:63583/api/createlog";

        public string GetChangeLog => "http://localhost:63583/api/getchangelog";

        public string GetAllLogs => "http://localhost:63583/api/getalllogs";

        public string DeleteLog => "http://localhost:63583/api/deletelog";

        public string UpdateLog => "http://localhost:63583/api/udpatelog";

        public string CreatePunishedLevel => "http://localhost:63583/api/createpunishmentlevel";

        public string GetPunishedLevel => "http://localhost:63583/api/getpunishedlevel";

        public string GetPunishedLevels => "http://localhost:63583/api/getpunishedlevels";

        public string DeletePunishedLevel => "http://localhost:63583/api/deletepunishedlevel";

        public string UpdatePunishedLevel => "http://localhost:63583/api/updatepunishedlevel";

        public string GetMember => "http://localhost:63583/api/getmember";

        public string GetAllMembers => "http://localhost:63583/api/getallmembers";
    }
}
