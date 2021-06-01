namespace ModBot.Domain.DTO
{
    public class PunishmentSettingsDto
    {

        public int Id { get; set; }
        public int TimeOutLevel { get; set; }
        public int KickLevel { get; set; }
        public int BanLevel { get; set; }
        public int SpamMuteTime { get; set; }
        public int StrikeMuteTime { get; set; }
        public ulong GuildId { get; set; }
    }
}
