using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class Guild : IGuild
    {

        #region Properties
        private ulong _id;
        private bool _hasBot;
        private string _avatar;
        private string _guildName;

        public ulong Id
        {
            get { return _id; }
            private set { }
        }

        public bool HasBot
        {
            get { return _hasBot; }
            private set { }
        }

        public string Avatar
        {
            get { return _avatar; }
            private set { }
        }

        public string GuildName
        {
            get { return _guildName; }
            private set { }
        }

        #endregion

        #region Constructors
        private Guild() { }

        
        public Guild(ulong id, bool hasBot, string avatar, string guildName)
        {
            _id = id;
            _hasBot = hasBot;
            _avatar = avatar;
            _guildName = guildName;
        }
        #endregion

        #region Methods

        #endregion

    }
}
