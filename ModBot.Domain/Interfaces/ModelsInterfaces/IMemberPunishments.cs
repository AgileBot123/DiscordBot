namespace ModBot.Domain.Models
{
    public interface IMemberPunishments
    {
        ulong MemberId { get;   }
         Member Member { get; }

        ulong PunishmentId { get;  }
         Punishment Punishment { get; }
    }
}