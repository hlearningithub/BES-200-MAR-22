using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Author = table.Column<string>(maxLength: 200, nullable: true),
                    Genre = table.Column<string>(maxLength: 200, nullable: true),
                    NumberOfPages = table.Column<int>(nullable: false),
                    InInventory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    For = table.Column<string>(nullable: true),
                    ReservationCreated = table.Column<DateTime>(nullable: false),
                    Book = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Genre", "InInventory", "NumberOfPages", "Title" },
                values: new object[] { 1, "Thoreau", "Philosophy", true, 322, "Walden" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Genre", "InInventory", "NumberOfPages", "Title" },
                values: new object[] { 2, "Franz Kafka", "Fiction", true, 180, "In the Penal Colony" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Genre", "InInventory", "NumberOfPages", "Title" },
                values: new object[] { 3, "Franz Kafka", "Fiction", true, 223, "The Trial" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
