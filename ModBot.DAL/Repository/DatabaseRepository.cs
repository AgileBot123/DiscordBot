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
            try
            {
                _context.Add(createBannedWord);
                return _context.SaveChanges() > 0;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public bool CreateChangelog(IChangelog createLog)
        {
            try
            {
                _context.Add(createLog);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateGetPunishment(IPunishmentsLevels createPunished)
        {
            try
            {
                _context.Add(createPunished);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBannedWord(IBannedWord bannedWord)
        {
            try
            {
                _context.Remove(bannedWord);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteChangelog(IChangelog changelog)
        {
            try
            {
                _context.Remove(changelog);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteGetPunishment(IPunishmentsLevels punishmentLevel)
        {
            try
            {
                _context.Remove(punishmentLevel);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<IBannedWord>> GetAllBannedWords()
        {
            try
            {
                var bannedWords = new List<IBannedWord>();
                foreach(var word in await _context.BannedWords.ToListAsync())
                {
                    var bannedword = new BannedWord(word.Word,
                                                      word.Strikes,
                                                       word.Punishment);
                    bannedWords.Add(bannedword);
                   
                }
                return bannedWords;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IChangelog>> GetAllChangelogs()
        {

            try
            {
                var changeLogs = new List<IChangelog>();
                foreach (var log in await _context.Changelogs.ToListAsync())
                {
                    var changelog = new Changelog(log.Id,
                                                   log.ChangedDate,
                                                   log.Changed
                                                   );
                    changeLogs.Add(changelog);
                }
                return changeLogs;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public Member GetUser(ulong id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IMember>> GetAllMembers()
        {
            try
            {
                var members = new List<IMember>();
                foreach (var _member in await _context.Members.ToListAsync())
                {
                    var member = new Member(_member.Id,
                                              _member.Strikes);
                    members.Add(member);

                }
                return members;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels()
        {

            try
            {
                var punishments = new List<IPunishmentsLevels>();
                foreach (var punishemntLevel in await _context.PunishmentsLevels.ToListAsync())
                {
                    var punishment = new PunishmentsLevels(punishemntLevel.Id,
                                                            punishemntLevel.TimeOutLevel,
                                                            punishemntLevel.KickLevel,
                                                            punishemntLevel.BanLevel,
                                                            punishemntLevel.SpamMuteTime,
                                                            punishemntLevel.StrikeMuteTime);
                    punishments.Add(punishment);
                   

                }
                return punishments;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IBannedWord> GetBannedWord(string word) => 
            await _context.BannedWords.Where(x => x.Word == word).SingleAsync();
        

        public async Task<IChangelog> GetChangelog(int id)
        {
            try
            {
               return await _context.Changelogs.Where(x => x.Id == id).SingleAsync();
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<IMember> GetMember(ulong id)
        {
            try
            {
                return await _context.Members.Where(x => x.Id == id).SingleAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IPunishmentsLevels> GetPunishment(int id)
        {
            try
            {
                return await _context.PunishmentsLevels.Where(x => x.Id == id).SingleAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public  bool UpdateBannedWord(IBannedWord updateBannedWord)
        {
           try
            {
                _context.Update(updateBannedWord);
                return _context.SaveChanges() > 0;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool UpdateChangelog(int id, IChangelog changelog)
        {
            try
            {
                _context.Update(changelog);
                return _context.SaveChanges() > 0;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public bool UpdateGetPunishment(IPunishmentsLevels updatePunished, int id)
        {
            try
            {
                _context.Update(updatePunished);
                return _context.SaveChanges() > 0 ;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
