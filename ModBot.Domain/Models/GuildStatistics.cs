using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class GuildStatistics : IGuildStatistics
    {
        private int _statisticsId;
        private ulong _guildId;
        public int StatisticsId
        {
            get { return _statisticsId; }
            private set { }
        }

        public Statistics Statistics { get; private set; }

        public ulong GuildId
        {
            get { return _guildId; }
            private set { }
        }

        public Guild Guild { get; private set; }
    }
}
