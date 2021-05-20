using ModBot.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModBot.Domain.Models
{
    public class Member : IMember
    {
        private int id;
        private int strikes;

        [Key]
        public int Id { get { return id; } private set { value = id; } }
        public int Strikes => strikes;

        private Member()
        {

        }


        public void AddStrikes(int strikes)
        {
            throw new NotImplementedException();
        }

        public void RemoveStrikes(int strikes)
        {
            throw new NotImplementedException();
        }
    }
}
