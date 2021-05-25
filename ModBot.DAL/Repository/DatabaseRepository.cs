using Microsoft.EntityFrameworkCore;
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
   public class DatabaseRepository :  IBannedWordRepository, IChangeLogRepository, IPunishmentsLevelsRepository, IMemberRepository
    {

        private readonly ModBotContext _context;
        public DatabaseRepository(ModBotContext context)
        {
            _context = context;
        }

        public bool CreateBannedWord(IBannedWord createBannedWord)
        {
            _context.Add(createBannedWord);
            return _context.SaveChanges() > 0; 
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

        public async Task<IEnumerable<IMember>> GetAllMembers() => await _context.Members.ToListAsync();

        public async Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels() => await _context.PunishmentsLevels.ToListAsync();

        public async Task<IBannedWord> GetBannedWord(string word) => 
            await _context.BannedWords.Where(x => x.Word == word).SingleAsync();
        

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
