using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.DTO.StatisticsDt
{
    public class StatisticsDto
    {
       public int NumberOfMembers { get; set; }
       public int NumberOfBannedWords { get;  set;}
       public int NumberOfMembersBeenTimedOut { get;  set;}
       public int NumberOfMembersBeingBanned { get;  set;}
       public  int TotalStrikesInDatabase { get; set; }
        public double AverageNumberOfStrikes { get;  set; }
        public double MedianNumberOfStrikes { get; set; }
       public string MostUsedCommand { get; set; }
    }
}
