using ModBot.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.DAL.Repository
{
   public  class DatabaseRepository
    {

        private readonly ModBotContext _ctx;
        public DatabaseRepository(ModBotContext ctx)
        {
            _ctx = ctx;
        }
    }
}
