using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Extensions.Routes
{
    public static class Routes
    {
        public const string Root = "api";
        private const string Base = Root + "/";

        public class Guilds
        {
            public const string CreateGuild = Base + "createguild";
            public const string GetGuild = Base + "getguild";
            public const string GetGuilds = Base + "getguilds";
            public const string UpdateGuild = Base + "updateguild";
        }

        public class BannedWords
        {
            public const string GetBannedWord = Base + "GetBannedWord";
            public const string GetAllBannedWords = Base + "GetAllBannedWords";
            public const string CreateBannedWord = Base + "CreateBannedWord";
            public const string DeleteBannedWord = Base + "DeleteBannedWord";
            public const string UpdateBannedWord = Base + "UpdateBannedWordList";

        }

        public class PunishmentLevels
        {
            public const string GetPunishmentLevel = Base + "GetPunishmentLevel/{id}";
            public const string GetPunishmentLevels = Base + "GetPunishmentLevels";
            public const string CreatePunishmentLevel = Base + "CreatePunishmentLevel";
            public const string DeletePunishmentLevel = Base + "DeletePunishmentLevel";
            public const string UpdatePunishmentLevel = Base + "UpdatePunishmentLevel";
        }
        public class Members
        {
            public const string GetMember = Base + "GetMember";
            public const string GetMembers = Base + "GetMembers";
        }
    }
}
