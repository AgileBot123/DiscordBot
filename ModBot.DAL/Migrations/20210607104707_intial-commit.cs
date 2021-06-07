using Microsoft.EntityFrameworkCore.Migrations;

namespace ModBot.DAL.Migrations
{
    public partial class intialcommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    StrikesAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punishment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BannedWord",
                columns: table => new
                {
                    Profanity = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Strikes = table.Column<int>(type: "int", nullable: false),
                    Punishment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannedWordUsedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedWord", x => new { x.Profanity, x.GuildId });
                    table.ForeignKey(
                        name: "FK_BannedWord_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PunishmentsLevel",
                columns: table => new
                {
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeOutLevel = table.Column<int>(type: "int", nullable: false),
                    KickLevel = table.Column<int>(type: "int", nullable: false),
                    BanLevel = table.Column<int>(type: "int", nullable: false),
                    SpamMuteTime = table.Column<int>(type: "int", nullable: false),
                    StrikeMuteTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PunishmentsLevel", x => x.GuildId);
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

            migrationBuilder.CreateIndex(
                name: "IX_BannedWord_GuildId",
                table: "BannedWord",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildPunishment_PunishmentId",
                table: "GuildPunishment",
                column: "PunishmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberPunishment_PunishmentId",
                table: "MemberPunishment",
                column: "PunishmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannedWord");

            migrationBuilder.DropTable(
                name: "GuildPunishment");

            migrationBuilder.DropTable(
                name: "MemberPunishment");

            migrationBuilder.DropTable(
                name: "PunishmentsLevel");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Punishment");

            migrationBuilder.DropTable(
                name: "Guild");
        }
    }
}
