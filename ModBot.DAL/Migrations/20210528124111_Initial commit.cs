using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModBot.DAL.Migrations
{
    public partial class Initialcommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannedWord",
                columns: table => new
                {
                    Profanity = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Strikes = table.Column<int>(type: "int", nullable: false),
                    Punishment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannedWordUsedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedWord", x => x.Profanity);
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
                name: "Guild",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    HasBot = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuildName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guild", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBot = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Punishment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrikesAmount = table.Column<int>(type: "int", nullable: false),
                    TimeOutUntil = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punishment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MostUsedCommand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfMembers = table.Column<int>(type: "int", nullable: false),
                    NumberOfBannedWords = table.Column<int>(type: "int", nullable: false),
                    NumberOfMembersBeenTimedOut = table.Column<int>(type: "int", nullable: false),
                    NumberOfMembersBeingBanned = table.Column<int>(type: "int", nullable: false),
                    TotalStrikesInDatabase = table.Column<int>(type: "int", nullable: false),
                    AverageNumberOfStrikes = table.Column<double>(type: "float", nullable: false),
                    MedianNumberOfStrikes = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BannedWordGuilds",
                columns: table => new
                {
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    BannedWordProfanity = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedWordGuilds", x => new { x.GuildId, x.BannedWordProfanity });
                    table.ForeignKey(
                        name: "FK_BannedWordGuilds_BannedWord_BannedWordProfanity",
                        column: x => x.BannedWordProfanity,
                        principalTable: "BannedWord",
                        principalColumn: "Profanity",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BannedWordGuilds_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PunishmentsLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOutLevel = table.Column<int>(type: "int", nullable: false),
                    KickLevel = table.Column<int>(type: "int", nullable: false),
                    BanLevel = table.Column<int>(type: "int", nullable: false),
                    SpamMuteTime = table.Column<int>(type: "int", nullable: false),
                    StrikeMuteTime = table.Column<int>(type: "int", nullable: false),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PunishmentsLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PunishmentsLevel_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildPunishment",
                columns: table => new
                {
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PunishmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildPunishment", x => new { x.GuildId, x.PunishmentId });
                    table.ForeignKey(
                        name: "FK_GuildPunishment_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildPunishment_Punishment_PunishmentId",
                        column: x => x.PunishmentId,
                        principalTable: "Punishment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberPunishment",
                columns: table => new
                {
                    MemberId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PunishmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPunishment", x => new { x.MemberId, x.PunishmentId });
                    table.ForeignKey(
                        name: "FK_MemberPunishment_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberPunishment_Punishment_PunishmentId",
                        column: x => x.PunishmentId,
                        principalTable: "Punishment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildStatistics",
                columns: table => new
                {
                    StatisticsId = table.Column<int>(type: "int", nullable: false),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildStatistics", x => new { x.GuildId, x.StatisticsId });
                    table.ForeignKey(
                        name: "FK_GuildStatistics_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildStatistics_Statistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "Statistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BannedWordGuilds_BannedWordProfanity",
                table: "BannedWordGuilds",
                column: "BannedWordProfanity");

            migrationBuilder.CreateIndex(
                name: "IX_GuildPunishment_PunishmentId",
                table: "GuildPunishment",
                column: "PunishmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildStatistics_StatisticsId",
                table: "GuildStatistics",
                column: "StatisticsId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberPunishment_PunishmentId",
                table: "MemberPunishment",
                column: "PunishmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PunishmentsLevel_GuildId",
                table: "PunishmentsLevel",
                column: "GuildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannedWordGuilds");

            migrationBuilder.DropTable(
                name: "Changelog");

            migrationBuilder.DropTable(
                name: "GuildPunishment");

            migrationBuilder.DropTable(
                name: "GuildStatistics");

            migrationBuilder.DropTable(
                name: "MemberPunishment");

            migrationBuilder.DropTable(
                name: "PunishmentsLevel");

            migrationBuilder.DropTable(
                name: "BannedWord");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Punishment");

            migrationBuilder.DropTable(
                name: "Guild");
        }
    }
}
