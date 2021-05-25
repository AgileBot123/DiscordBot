using ModBot.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModBot.Domain.Models
{
    public class Member : IMember
    {
        private ulong id;
        private int strikes;

        public ulong Id => id;
        public int Strikes => strikes;

        private Member() {}

        public Member(ulong id, int strikes)
        {
            this.id = id;
            this.strikes = strikes;
        }


        public void AddStrikes(int strikes)
        {
            this.strikes += strikes;
        }

        public void RemoveStrikes(int strikes)
        {
            this.strikes -= strikes;
        }
    }
}
