﻿using Microsoft.EntityFrameworkCore;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.DAL.Data
{
    public class ModBotContext : DbContext
    {

        public ModBotContext(DbContextOptions<ModBotContext> options) : base(options){}



        public DbSet<Member> Members { get; set; }
        public DbSet<BannedWord> BannedWords { get; set; }
        public  DbSet<Changelog> Changelogs { get; set; }
        public DbSet<PunishmentsLevels> PunishmentsLevels { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Members
            modelBuilder.Entity<Member>().HasKey(p => new { p.Id });          
            modelBuilder.Entity<Member>().Property(b => b.Id).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<Member>().Property(b => b.Strikes).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

            //BannedWord
            modelBuilder.Entity<BannedWord>().Property(b => b.Id).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<BannedWord>().Property(b => b.Word).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<BannedWord>().Property(b => b.Strikes).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<BannedWord>().Property(b => b.Punishment).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

            //ChangedLogs
            modelBuilder.Entity<Changelog>().Property(b => b.Id).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<Changelog>().Property(b => b.Changed).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<Changelog>().Property(b => b.ChangedDate).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

            //PunishmentLevels
            modelBuilder.Entity<PunishmentsLevels>().Property(b => b.Id).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<PunishmentsLevels>().Property(b => b.TimeOutLevel).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<PunishmentsLevels>().Property(b => b.KickLevel).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<PunishmentsLevels>().Property(b => b.BanLevel).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<PunishmentsLevels>().Property(b => b.SpamMuteTime).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<PunishmentsLevels>().Property(b => b.StrikeMuteTime).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        }
    }
}
