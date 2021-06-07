using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Models.Endpoints
{
    public class Endpoints : IEndpoints
    {
        public string Host => "https://localhost:44396/";
        public string Base => Host + "api/";

        #region Guild
        public string GetGuild => Base + "getguild";
        public string CreateGuild => Base + "createguild";
        public string UpdateGuild => Base + "updateguild";
        #endregion

        #region Banned Word
        public string CreateBannedWord => Base+"createbannedword";
        public string GetBannedWord => Base+"getbannedword/";
        public string GetAllBannedWords => Base + "getallbannedwords";
        public string DeleteBannedWord => Base + "deletebannedword/";
        public string UpdateBannedWordList => Base + "UpdateBannedWordList";
        #endregion

        #region Logs
        public string CreateLog => Base + "createlog";
        public string GetChangeLog => Base + "getchangelog";
        public string GetAllLogs => Base + "getalllogs";
        public string DeleteLog => Base + "deletelog";
        public string UpdateLog => Base + "updatelog";
        #endregion

        #region Punishment level
        public string CreatePunishmentLevel => Base + "createpunishmentlevel";
        public string GetPunishmentLevel => Base + "getpunishmentlevel";
        public string GetPunishmentLevels => Base + "getpunishmentlevels";
        public string DeletePunishmentLevel => Base + "deletepunishmentlevel/";
        public string UpdatePunishmentLevel => Base + "updatepunishmentlevel";
        #endregion

        #region Member
        public string GetMember => Base + "getmember/";
        public string GetAllMembers => Base + "getallmembers";
        #endregion
    }
}
