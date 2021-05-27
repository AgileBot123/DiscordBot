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
        public DbSet<PunishmentsLevel> PunishmentsLevels { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ////Members
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Member>().HasKey(p => new { p.Id });

            ////BannedWord
            modelBuilder.Entity<BannedWord>().ToTable("BannedWord");
            modelBuilder.Entity<BannedWord>().HasKey(p => new { p.Word });

            ////ChangedLogs
            modelBuilder.Entity<Changelog>().ToTable("Changelog");
            modelBuilder.Entity<Changelog>().HasKey(p => new { p.Id });

            ////PunishmentLevels
            modelBuilder.Entity<PunishmentsLevel>().HasKey(p => new { p.Id });
            modelBuilder.Entity<PunishmentsLevel>().ToTable("PunishmentsLevel");
        }
    }
}
