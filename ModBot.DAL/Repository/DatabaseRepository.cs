using ModBot.DAL.Data;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.DAL.Repository
{
   public class DatabaseRepository : ICommandLogicRepository
    {

       private readonly ModBotContext _context;
        public DatabaseRepository(ModBotContext context)
        {
            _context = context;
        }
    }
}
