using ModBot.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ModBot.Domain.Models
{
    public class Member : IMember
    {
        private ulong _id;
        private int _strikes;

        public ulong Id 
        { 
            get { return _id; }
            private set { }
        }
        public int Strikes
        {
            get { return _strikes; }
            private set { }
        }
        private Member() {}

        public Member(ulong id, int strikes)
        {
            this._id = id;
            this._strikes = strikes;
        }


        public void AddStrikes(int strikes)
        {
            this._strikes += strikes;
        }

        public void RemoveStrikes(int strikes)
        {
            this._strikes -= strikes;
        }
    }
}
