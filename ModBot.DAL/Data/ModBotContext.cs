using Microsoft.EntityFrameworkCore;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot.DAL.Data
{
    public class ModBotContext : DbContext
    {


        public ModBotContext(DbContextOptions<ModBotContext> options) : base(options){}

        public DbSet<Member> Members { get; set; }
        public DbSet<BannedWord> BannedWords { get; set; }
        public DbSet<Changelog> Changelogs { get; set; }
        public DbSet<PunishmentSettings> PunishmentsLevels { get; set; }
        public DbSet<Punishment> Punishments { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<GuildPunishment> GuildPunishment { get; set; }
        public DbSet<MemberPunishment> MemberPunishments { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<GuildStatistics> GuildStatistics { get; set; }
        public DbSet<BannedWordGuilds> BannedWordGuilds { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Guild
            modelBuilder.Entity<Guild>().ToTable("Guild");
            modelBuilder.Entity<Guild>().HasKey(p => new { p.Id });

            //GuildPunishment
            modelBuilder.Entity<GuildPunishment>().ToTable("GuildPunishment");
            modelBuilder.Entity<GuildPunishment>().HasKey(o => new { o.GuildId, o.PunishmentId});

            //Punishment
            modelBuilder.Entity<Punishment>().ToTable("Punishment");
            modelBuilder.Entity<Statistics>().HasKey(p => new { p.Id });


            //MemberPunishment
            modelBuilder.Entity<MemberPunishment>().ToTable("MemberPunishment");
            modelBuilder.Entity<MemberPunishment>().HasKey(o => new { o.MemberId, o.PunishmentId });

            //GuildStatistics
            modelBuilder.Entity<GuildStatistics>().ToTable("GuildStatistics");
            modelBuilder.Entity<GuildStatistics>().HasKey(o => new { o.GuildId, o.StatisticsId });
            //BannedWordGuilds
            modelBuilder.Entity<BannedWordGuilds>().ToTable("BannedWordGuilds");
            modelBuilder.Entity<BannedWordGuilds>().HasKey(o => new { o.GuildId, o.BannedWordProfanity });

            //Statistics 
            modelBuilder.Entity<Statistics>().ToTable("Statistics");
            modelBuilder.Entity<Statistics>().HasKey(p => new { p.Id });

            ////Members
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Member>().HasKey(p => new { p.Id });

            ////BannedWord
            modelBuilder.Entity<BannedWord>().ToTable("BannedWord");
            modelBuilder.Entity<BannedWord>().HasKey(p => new { p.Profanity });

            ////ChangedLogs
            modelBuilder.Entity<Changelog>().ToTable("Changelog");
            modelBuilder.Entity<Changelog>().HasKey(p => new { p.Id });

            ////PunishmentLevels
            modelBuilder.Entity<PunishmentSettings>().HasKey(p => new { p.Id });
            modelBuilder.Entity<PunishmentSettings>().ToTable("PunishmentsLevel");
        }
    }
}
