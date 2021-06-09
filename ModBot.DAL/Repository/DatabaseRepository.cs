using Microsoft.EntityFrameworkCore;
using ModBot.DAL.Data;
using ModBot.Domain.DTO.BannedWordDtos;
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
   public class DatabaseRepository :  IBannedWordRepository,IPunishmentsLevelsRepository, IMemberRepository,
              IPunishmentRepository, IMemberPunishmentRepository, IGuildPunishmentRepository, IGuildRepository
    {
     
        private readonly ModBotContext _context;
        public DatabaseRepository(ModBotContext context)
        {
            _context = context;
        }

        #region Guild
        public virtual async Task<IGuild> GetGuild(ulong guildID)
        {
            try
            {
                return await _context.Guilds.AsNoTracking().SingleAsync(x => x.Id == guildID);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<IGuild>> GetAllGuilds()
        {
            try
            {
                var test = await _context.Guilds.AsNoTracking().ToListAsync();
                return test;
                //var guilds = new List<IGuild>();
                //foreach (var _guild in await _context.Guilds.ToListAsync())
                //{
                //    var guild = new Guild(_guild.Id, _guild.HasBot, _guild.Avatar, _guild.GuildName);
                //    guilds.Add(guild);
                //}
                //return guilds;
            }
            catch (Exception)
            {
                return new List<IGuild>();
            }
        }

        public virtual bool CreateGuild(IGuild guild)
        {
            _context.Add(guild);
            return _context.SaveChanges() > 0;
        }

        public virtual bool UpdateGuild(IGuild guild)
        {
            _context.Update(guild);
            return _context.SaveChanges() > 0;
        }

        #endregion

        #region Member
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
        public virtual async Task<IMember> GetMember(ulong id)
        {
            try
            {
                return await _context.Members.AsNoTracking().SingleAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<IMember>> GetAllMembers()
        {
            try
            {
                var members = new List<IMember>();
                foreach (var _member in await _context.Members.AsNoTracking().ToListAsync())
                {
                    var member = new Member(_member.Id,
                                              _member.Username,
                                              _member.Avatar,
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
        #endregion
 
        #region Banned Word
        public virtual bool CreateBannedWord(IBannedWord createBannedWord)
        {
            try
            {
                _context.Add(createBannedWord);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
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
        public virtual async Task<IEnumerable<IBannedWord>> GetAllBannedWords(ulong guildId)
        {
            try
            {
                var bannedWords = new List<IBannedWord>();
                foreach (var word in await _context.BannedWords.AsNoTracking().Where(x => x.GuildId == guildId).ToListAsync())
                {
                    var bannedword = new BannedWord(
                                        word.Profanity, word.Strikes,
                                        word.Punishment,
                                        word.BannedWordUsedCount,
                                        word.GuildId);
                    bannedWords.Add(bannedword);

                }
                return bannedWords;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public virtual async Task<IBannedWord> GetBannedWord(ulong guildId, string word)
        {
            try
            {
                return await _context.BannedWords.AsNoTracking().Where(x => x.Profanity == word 
                                && x.GuildId == guildId).SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual bool UpdateBannedWord(IBannedWord updateBannedWord)
        {
            try
            {
                _context.Update(updateBannedWord);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion    

        #region Punishment Settings
        public virtual bool CreatePunishmentSetting(IPunishmentsLevels createPunished)
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
        public virtual bool DeletePunishmentSetting(IPunishmentsLevels punishmentLevel)
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
        public virtual async Task<IPunishmentsLevels> GetPunishmentLevels(ulong guildId)
        {
            try
            {
                var punishmentlevel = await _context.PunishmentsLevels.AsNoTracking().Where(x => x.GuildId == guildId).FirstOrDefaultAsync();
                
                return punishmentlevel;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public virtual async Task<IPunishmentsLevels> GetPunishmentSetting(ulong guilId)
        {
            try
            {
                return await _context.PunishmentsLevels.AsNoTracking().SingleAsync(x => x.GuildId==guilId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public virtual bool UpdatePunishmentSetting(IPunishmentsLevels updatePunished, int id)
        {
            try
            {
                _context.Update(updatePunished);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Punishment
        public async Task<Punishment> CreatePunishment()
        {
            try
            {
                var punishment = new Punishment();
                var result = _context.Add(punishment);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Punishment> GetAllPunishments()
        {
            try
            {
               var punishmentList =  _context.Punishments.ToList();

                foreach (var punish in punishmentList)
                {
                    _context.Entry<Punishment>(punish).State = EntityState.Detached;
                }

                return punishmentList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Punishment>> GetAllPunishmentsAsync()
        {
            try
            {

                var punishementsList =  _context.Punishments.ToList();

                foreach (var punishments in punishementsList)
                {
                    _context.Entry<Punishment>(punishments).State = EntityState.Detached;
                }

                return punishementsList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<bool> UpdatePunishment(Punishment memberPunishment)
        {
            try
            {
                _context.Update(memberPunishment);
                return  _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        #endregion

        #region Member Punishment
        public async Task<bool> AddToMemberPunishment(ulong memberId, int punishedId)
        {
            try
            {
                var newMemberPunished = new MemberPunishment(memberId, punishedId);
                _context.Add(newMemberPunished);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task GetMemberPunishment(ulong memberId, ulong guildId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MemberPunishment>> GetAllMemberPunishments()
        {
            try
            {
                return await _context.MemberPunishments.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Guild Punishment
        public async Task<bool> CreateGuildPunishment(int punishmentId, ulong guildId)
        {
            try
            {
                var createNewGuildPunishment = new GuildPunishment(guildId, punishmentId);
                _context.Add(createNewGuildPunishment);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GuildPunishment> GetAllGuildPunishments()
        {
            try
            {
                 var AllGuildPunishment = _context.GuildPunishment.ToList();

                foreach (var guildPunishment in AllGuildPunishment)
                {
                    _context.Entry<GuildPunishment>(guildPunishment).State = EntityState.Detached;
                }

                return AllGuildPunishment;
            }
            catch (Exception)
            {

                throw;
            }
        }




        #endregion




    }
}
