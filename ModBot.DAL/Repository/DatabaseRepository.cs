using ModBot.DAL.Data;
using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.DAL.Repository
{
   public class DatabaseRepository : ICommandLogicRepository, IBannedWordRepository, IChangeLogRepository, IPunishmentsLevelsRepository, IMemberRepository
    {

        private readonly ModBotContext _context;
        public DatabaseRepository(ModBotContext context)
        {
            _context = context;
        }

        public bool CreateBannedWord(IBannedWord createBannedWord)
        {
            throw new NotImplementedException();
        }

        public bool CreateChangelog(IChangelog createLog)
        {
            throw new NotImplementedException();
        }

        public bool CreateGetPunishment(IPunishmentsLevels createPunished)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBannedWord(IBannedWord bannedWord)
        {
            throw new NotImplementedException();
        }

        public bool DeleteChangelog(IChangelog changelog)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGetPunishment(IPunishmentsLevels punishmentLevel)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IBannedWord>> GetAllBannedWords()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IChangelog>> GetAllChangelogs()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMember>> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels()
        {
            throw new NotImplementedException();
        }

        public Task<IBannedWord> GetBannedWord(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IChangelog> GetChangelog(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IMember> GetMember(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<IPunishmentsLevels> GetPunishment(int id)
        {
            throw new NotImplementedException();
        }

        public Member GetUser(ulong id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBannedWord(IBannedWord updateBannedWord, int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateChangelog(int id, IChangelog changelog)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGetPunishment(IPunishmentsLevels updatePunished, int id)
        {
            throw new NotImplementedException();
        }
    }
}
