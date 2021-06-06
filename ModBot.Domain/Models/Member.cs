using ModBot.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ModBot.Domain.Models
{
    public class Member : IMember
    {

        #region Properties
        private readonly ulong _id;
        private string _username;
        private string _avatar;
        private bool _isBot;

        public ulong Id
        {
            get { return _id; }
            private set { }
        }

        public string Avatar
        {
            get { return _avatar; }
            private set { }
        }


        public string Username
        {
            get { return _username; }
            private set { }
        }

        public bool IsBot
        {
            get { return _isBot; }
            private set { }
        }
        #endregion


        #region Constructors
        private Member() { }

        public Member(ulong id, string username, string avatar,bool isBot)
        {
            this._id = id;
            _username = username;
            _avatar = avatar;
            _isBot = isBot;
        }
        #endregion


    }
}
