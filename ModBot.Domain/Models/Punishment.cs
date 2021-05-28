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
        private int _timeOutUntil;

        public int Id
        {
            get { return _id; }
            private set { }
        }

        public int StrikesAmount
        {
            get { return _strikesAmount; }
            private set { }
        }

        public int TimeOutUntil
        {
            get { return _timeOutUntil; }
            private set { }
        }
        #endregion

        #region COnstructors
        private Punishment() { }

        public Punishment(int id, int strikesAmount, int timeOutUntil)
        {
            this._id = id;
            this._strikesAmount = strikesAmount;
            this._timeOutUntil = timeOutUntil;
        }

        public Punishment(int strikesAmount = 0, int timeOutUntil = 0)
        {
            this._strikesAmount = strikesAmount;
            this._timeOutUntil = timeOutUntil;
        }
        #endregion

    }
}
