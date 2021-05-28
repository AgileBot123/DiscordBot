using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class Punishment : IPunishment
    {
        private ulong _id;
        private int _strikesAmount;
        private int _timeOutUntil;

        public ulong Id
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

        private Punishment(){}
        
        public Punishment(ulong id, int strikesAmount, int timeOutUntil)
        {
            this._id = id;
            this._strikesAmount = strikesAmount;
            this._timeOutUntil = timeOutUntil;
        }
    }
}
