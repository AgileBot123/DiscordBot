﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ModBot.DAL.Migrations
{
    public partial class Changedafew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BannedWord",
                table: "BannedWord");

            migrationBuilder.AlterColumn<string>(
                name: "Profanity",
                table: "BannedWord",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "BannedWord",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BannedWord",
                table: "BannedWord",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BannedWord",
                table: "BannedWord");

            migrationBuilder.DropColumn(
                name: "id",
                table: "BannedWord");

            migrationBuilder.AlterColumn<string>(
                name: "Profanity",
                table: "BannedWord",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BannedWord",
                table: "BannedWord",
                column: "Profanity");
        }
    }
}
