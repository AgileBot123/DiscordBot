using Microsoft.EntityFrameworkCore;
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
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().Property(b => b.Id).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            modelBuilder.Entity<Member>().Property(b => b.Strikes).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        }



    }
}
