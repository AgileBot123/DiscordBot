﻿using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class GuildPunishment : IGuildPunishment
    {

        #region Properties
        private readonly ulong _guildId;
        private readonly ulong _punishmentId;
        public ulong GuildId
        {
            get { return _guildId; }
            private set { }
        }

        public Guild Guild { get; private set; }

        public ulong PunishmentId
        {
            get { return _punishmentId; }
            private set { }
        }

        public Punishment Punishment { get; private set; }
        #endregion


        #region Constructors

        private GuildPunishment(){ }

        public GuildPunishment(ulong guildId, ulong punishmentId)
        {
            _guildId = guildId;
            _punishmentId = punishmentId;
        }
        #endregion
    }
}
