using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModBot.DAL.Migrations
{
    public partial class initialcommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannedWord",
                columns: table => new
                {
                    Word = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Strikes = table.Column<int>(type: "int", nullable: false),
                    Punishment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedWord", x => x.Word);
                });

            migrationBuilder.CreateTable(
                name: "Changelog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Changed = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changelog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Strikes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PunishmentsLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOutLevel = table.Column<int>(type: "int", nullable: false),
                    KickLevel = table.Column<int>(type: "int", nullable: false),
                    BanLevel = table.Column<int>(type: "int", nullable: false),
                    SpamMuteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StrikeMuteTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PunishmentsLevels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannedWord");

            migrationBuilder.DropTable(
                name: "Changelog");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "PunishmentsLevels");
        }
    }
}
