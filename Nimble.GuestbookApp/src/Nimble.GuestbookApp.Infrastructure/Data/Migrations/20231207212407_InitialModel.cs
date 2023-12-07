using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nimble.GuestbookApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guestbooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guestbooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestbookEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    DateTimeCreated = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    GuestbookId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestbookEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestbookEntry_Guestbooks_GuestbookId",
                        column: x => x.GuestbookId,
                        principalTable: "Guestbooks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestbookEntry_GuestbookId",
                table: "GuestbookEntry",
                column: "GuestbookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contributors");

            migrationBuilder.DropTable(
                name: "GuestbookEntry");

            migrationBuilder.DropTable(
                name: "Guestbooks");
        }
    }
}
