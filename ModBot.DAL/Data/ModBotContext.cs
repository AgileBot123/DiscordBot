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
        public DbSet<PunishmentSettings> PunishmentsLevels { get; set; }
        public DbSet<Punishment> Punishments { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<GuildPunishment> GuildPunishment { get; set; }
        public DbSet<MemberPunishment> MemberPunishments { get; set; }


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
            modelBuilder.Entity<Punishment>().HasKey(o => new { o.Id });

            //MemberPunishment
            modelBuilder.Entity<MemberPunishment>().ToTable("MemberPunishment");
            modelBuilder.Entity<MemberPunishment>().HasKey(o => new { o.MemberId, o.PunishmentId });


            ////Members
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Member>().HasKey(p => new { p.Id });

            ////BannedWord
            modelBuilder.Entity<BannedWord>().ToTable("BannedWord");
            modelBuilder.Entity<BannedWord>().HasKey(p => new { p.Profanity, p.GuildId});

            ////PunishmentLevels
            modelBuilder.Entity<PunishmentSettings>().ToTable("PunishmentsLevel");
            modelBuilder.Entity<PunishmentSettings>().HasKey(p => p.GuildId);
        }
    }
}
