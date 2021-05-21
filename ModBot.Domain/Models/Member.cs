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

        [Key]
        public ulong Id => id;
        public int Strikes => strikes;

        private Member() {}


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
