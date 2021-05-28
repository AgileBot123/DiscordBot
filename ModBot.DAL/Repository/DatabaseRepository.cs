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
   public class DatabaseRepository :  IBannedWordRepository, IChangeLogRepository, IPunishmentsLevelsRepository, IMemberRepository, IStatisticsRepository
    {
        public DatabaseRepository()
        {

        }
        private readonly ModBotContext _context;
        public DatabaseRepository(ModBotContext context)
        {
            _context = context;
        }

        public bool AddMember(IMember member)
        {
            try
            {
                _context.Add(member);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddToStatistics(IStatistics stats)
        {
            try
            {
                _context.Add(stats);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool CreateBannedWord(IBannedWord createBannedWord)
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

        public virtual bool CreateChangelog(IChangelog createLog)
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

        public virtual bool CreatePunishment(IPunishmentsLevels createPunished)
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


        public virtual bool DeleteBannedWord(IBannedWord bannedWord)
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

        public virtual bool DeleteChangelog(IChangelog changelog)
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

        public virtual bool DeletePunishment(IPunishmentsLevels punishmentLevel)
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

        public virtual async Task<IEnumerable<IBannedWord>> GetAllBannedWords()
        {
            try
            {
                var bannedWords = new List<IBannedWord>();
                foreach(var word in await _context.BannedWords.ToListAsync())
                {
                    var bannedword = new BannedWord(
                                        word.Word,                                         word.Strikes,
                                        word.Punishment,
                                        word.BannedWordUsedCount);
                    bannedWords.Add(bannedword);
                   
                }
                return bannedWords;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<IChangelog>> GetAllChangelogs()
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

        public virtual async Task<IEnumerable<IMember>> GetAllMembers()
        {
            try
            {
                var members = new List<IMember>();                
                foreach (var _member in await _context.Members.ToListAsync())
                {
                    var member = new Member(_member.Id,
                                              _member.Username,
                                              _member.Avatar,
                                              _member.Email,
                                              _member.IsBot);
                    members.Add(member);
                }
                return members;
            }
            catch (Exception)
            {
                return new List<IMember>();
            }
        }

        public virtual async Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels()
        {

            try
            {
                var punishments = new List<IPunishmentsLevels>();
                foreach (var punishemntLevel in await _context.PunishmentsLevels.ToListAsync())
                {
                    var punishment = new PunishmentSettings(punishemntLevel.Id,
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

        public virtual async Task<IBannedWord> GetBannedWord(string word) =>
            await _context.BannedWords.SingleAsync(x => x.Word == word);



        public virtual async Task<IChangelog> GetChangelog(int id)
        {
            try
            {
                return await _context.Changelogs.SingleAsync(x => x.Id == id);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public virtual async Task<IMember> GetMember(ulong id)
        {
            try
            {
                return await _context.Members.SingleAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<IPunishmentsLevels> GetPunishment(int id)
        {
            try
            {
                return await _context.PunishmentsLevels.SingleAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IStatistics>> GetAllStatistics()
        {
            try
            {
                var newList = new List<IStatistics>();
                foreach (var item in await _context.Statistics.ToListAsync())
                {
                    newList.Add(new Statistics(
                        item.Id,
                        item.NumberOfMembers,
                        item.NumberOfBannedWords,
                        item.NumberOfMembersBeenTimedOut,
                        item.NumberOfMembersBeingBanned,
                        item.TotalStrikesInDatabase,
                        item.AverageNumberOfStrikes,
                        item.MedianNumberOfStrikes,
                        item.MostUsedCommand));
                }
                return newList;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IStatistics> GetStatistics(int id)
        {
            return await _context.Statistics.SingleAsync(x => x.Id == id);
        }

        public virtual bool UpdateBannedWord(IBannedWord updateBannedWord)
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

        public virtual bool UpdateChangelog(int id, IChangelog changelog)
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

        public virtual bool UpdatePunishment(IPunishmentsLevels updatePunished, int id)
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
