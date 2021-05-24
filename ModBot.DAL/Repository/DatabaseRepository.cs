using ModBot.DAL.Data;
using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.DAL.Repository
{
   public class DatabaseRepository : ICommandLogicRepository, IBannedWordRepository, IChangeLogRepository, IPunishmentsLevelsRepository, IMemberRepository
    {

        private readonly ModBotContext _context;
        public DatabaseRepository(ModBotContext context)
        {
            _context = context;
        }



        public void Create(IBannedWord createBannedWord)
        {
            throw new NotImplementedException();
        }

        public void Create(IChangelog createLog)
        {
            throw new NotImplementedException();
        }

        public void Create(IPunishmentsLevels createPunished)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IBannedWord Get(int id)
        {
            throw new NotImplementedException();
        }

        public IMember Get(ulong id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBannedWord> GetAll()
        {
            throw new NotImplementedException();
        }

        public IChangelog Update(IBannedWord updateBannedWord, int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, IChangelog changelog)
        {
            throw new NotImplementedException();
        }

        public void Update(IPunishmentsLevels updatePunished, int id)
        {
            throw new NotImplementedException();
        }

        IChangelog IChangeLogRepository.Get(int id)
        {
            throw new NotImplementedException();
        }

        IPunishmentsLevels IPunishmentsLevelsRepository.Get(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IChangelog> IChangeLogRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IPunishmentsLevels> IPunishmentsLevelsRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IMember> IMemberRepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
