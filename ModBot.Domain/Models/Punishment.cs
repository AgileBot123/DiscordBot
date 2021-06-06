using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class Punishment : IPunishment
    {
        #region Properties
        private int _id;
        private int _strikesAmount;

        public int Id
        {
            get { return _id; }
            private set { }
        }

        public int StrikesAmount
        {
            get { return _strikesAmount; }
            set { _strikesAmount = value; }
        }

        #endregion

        #region COnstructors
        private Punishment() { }

        public Punishment(int id, int strikesAmount)
        {
            this._id = id;
            this._strikesAmount = strikesAmount;
        }

        public Punishment(int strikesAmount = 0)
        {
            this._strikesAmount = strikesAmount;
        }
        #endregion

    }
}
