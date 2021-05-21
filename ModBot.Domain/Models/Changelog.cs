using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class Changelog : IChangelog
    {
        private int id;
        private DateTime changedDate;
        private string changed;


        public int Id => id;
        public DateTime ChangedDate => changedDate;
        public string Changed => changed;
    }
}
