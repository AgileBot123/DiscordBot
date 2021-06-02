using ModBot.Domain.interfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces
{
    public interface IStatistics
    {
        int Id { get; }
        int NumberOfMembers { get; }
        int NumberOfBannedWords { get; }
        int NumberOfTimesBannedWordBeenUsed { get; }
        int NumberOfTimesEachCommandoBeenUsed { get; }
        int TotalStrikesInDatabase { get; }
        double AverageNumberOfStrikes { get; }
        double MedianNumberOfStrikes { get; }

        ulong GuildId { get; }
        Guild Guild { get; }


        double MedianStrikePerMember(List<IMember> members);
        double AverageStrikesPerMember(List<IMember> members, int totalStrikesInDatabase);
    }
}
