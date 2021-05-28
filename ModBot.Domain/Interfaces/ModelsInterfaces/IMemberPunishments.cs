namespace ModBot.Domain.Models
{
    public interface IMemberPunishments
    {
        ulong MemberId { get;   }
         Member Member { get; }

        int PunishmentId { get;  }
         Punishment Punishment { get; }
    }
}