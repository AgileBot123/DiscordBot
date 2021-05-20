using ModBot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class PunishedLevel : IPunishmentsLevels
    {
        public int TimeOut { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Kick { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Ban { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
