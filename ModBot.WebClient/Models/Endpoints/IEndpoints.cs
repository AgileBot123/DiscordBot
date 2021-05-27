namespace ModBot.WebClient.Models.Endpoints
{
    public interface IEndpoints
    {
        string CreateBannedWord { get; }

        string GetBannedWord { get; }

        string GetAllBannedWords { get; }

        string DeleteBannedWord { get; }

        string UpdateBannedWordList { get; }

        string CreateLog { get; }

        string GetChangeLog { get; }

        string GetAllLogs { get; }

        string DeleteLog { get; }

        string UpdateLog { get; }

        string CreatePunishedLevel { get; }

        string GetPunishedLevel { get; }

        string GetPunishedLevels { get; }

        string DeletePunishedLevel { get; }

        string UpdatePunishedLevel { get; }
        string GetMember { get; }
        string GetAllMembers { get; }
        string  Host { get; }

    }
}
