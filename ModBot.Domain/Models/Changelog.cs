using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class Changelog : IChangelog
    {
        private readonly int _id;
        private readonly DateTime _changedDate;
        private readonly string _changed;


        public int Id
        {
            get { return _id; }
            private set { }
        }
        public DateTime ChangedDate
        {
            get { return _changedDate; }
            private set { }
        }
        public string Changed
        {
            get { return _changed; }
            private set { }
        }

        private Changelog() { }

        public Changelog(DateTime changeDate, string changed)
        {
            this._changedDate = changeDate;
            this._changed = changed;
        }
        public Changelog(int id, DateTime changeDate, string changed)
        {
            this._id = id;
            this._changedDate = changeDate;
            this._changed = changed;
        }
    }
}
