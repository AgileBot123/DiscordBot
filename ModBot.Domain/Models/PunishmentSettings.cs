using ModBot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModBot.Domain.Models
{
   
    public class PunishmentSettings : IPunishmentsLevels
    {
        #region Properties
        private readonly int _timeOutLevel;
        private readonly int _kickLevel;
        private readonly int _banLevel;
        private readonly int _spamMuteTime;
        private readonly int _strikeMuteTime;
        private readonly ulong _guildId; 


        public int TimeOutLevel
        {
            get { return _timeOutLevel; }
            private set { }
        }
        public int KickLevel
        {
            get { return _kickLevel; }
            private set { }
        }
        public int BanLevel
        {
            get { return _banLevel; }
            private set { }
        }
 
        public int SpamMuteTime
        {
            get { return _spamMuteTime; }
            private set { }
        }

        public int StrikeMuteTime
        {
            get { return _strikeMuteTime; }
            private set { }
        }


        [Key, ForeignKey(nameof(Guild))]
        public ulong GuildId
        {
            get { return _guildId; }
            private set { }
        }
        public Guild Guild { get; private set; }
        #endregion


        #region Constructors
        private PunishmentSettings() { }

        public PunishmentSettings(int timeoutLevel, int kickLevel, int banLevel, int spamMuteLevel, int strikeMuteLevel, ulong guildId)
        {
            this._timeOutLevel = timeoutLevel;
            this._kickLevel = kickLevel;
            this._banLevel = banLevel;
            this._spamMuteTime = spamMuteLevel;
            this._strikeMuteTime = strikeMuteLevel;
            this._guildId = guildId;
        }


        public PunishmentSettings(int timeoutLevel, int kickLevel, int banLevel, int spamMuteLevel, int strikeMuteLevel)
        {
            this._timeOutLevel = timeoutLevel;
            this._kickLevel = kickLevel;
            this._banLevel = banLevel;
            this._spamMuteTime = spamMuteLevel;
            this._strikeMuteTime = strikeMuteLevel;
        }
        #endregion

    }
}
