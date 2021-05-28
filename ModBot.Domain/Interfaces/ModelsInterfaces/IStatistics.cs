using ModBot.Domain.interfaces;
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
        int NumberOfMembersBeenTimedOut { get; }
        int NumberOfMembersBeingBanned { get; }
        int TotalStrikesInDatabase { get; }
        double AverageNumberOfStrikes { get; }
        double MedianNumberOfStrikes { get; }
        string MostUsedCommand { get; }


        double MedianStrikePerMember(List<IMember> members);
        double AverageStrikesPerMember(List<IMember> members, int totalStrikesInDatabase);
    }
}
