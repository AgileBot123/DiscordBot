﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModBot.DAL.Data;

namespace ModBot.DAL.Migrations
{
    [DbContext(typeof(ModBotContext))]
    partial class ModBotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ModBot.Domain.Models.BannedWord", b =>
                {
                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Punishment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Strikes")
                        .HasColumnType("int");

                    b.HasKey("Word");

                    b.ToTable("BannedWord");
                });

            modelBuilder.Entity("ModBot.Domain.Models.Changelog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Changed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ChangedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Changelog");
                });

            modelBuilder.Entity("ModBot.Domain.Models.Member", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

                    b.Property<int>("Strikes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("ModBot.Domain.Models.PunishmentsLevels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BanLevel")
                        .HasColumnType("int");

                    b.Property<int>("KickLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("SpamMuteTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StrikeMuteTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TimeOutLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PunishmentsLevels");
                });
#pragma warning restore 612, 618
        }
    }
}
