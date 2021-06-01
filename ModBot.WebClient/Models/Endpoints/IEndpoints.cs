namespace ModBot.WebClient.Models.Endpoints
{
    public interface IEndpoints
    {
        string CreateGuild { get; }
        string UpdateGuild { get; }

        string CreateBannedWord { get; }

        string GetBannedWord { get; }

        string GetAllBannedWords { get; }

        string DeleteBannedWord { get; }
        
        string GetGuild { get; }

        string UpdateBannedWordList { get; }

        string CreateLog { get; }

        string GetChangeLog { get; }

        string GetAllLogs { get; }

        string DeleteLog { get; }

        string UpdateLog { get; }

        string CreatePunishmentLevel { get; }

        string GetPunishmentLevel { get; }

        string GetPunishmentLevels { get; }

        string DeletePunishmentLevel { get; }

        string UpdatePunishmentLevel { get; }
        string GetMember { get; }
        string GetAllMembers { get; }
        string  Host { get; }

    }
}
