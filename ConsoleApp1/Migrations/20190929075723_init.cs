using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    ISBN = table.Column<Guid>(nullable: false),
                    Publisher = table.Column<string>(nullable: true),
                    PrintDate = table.Column<DateTime>(nullable: false),
                    CopyNumber = table.Column<int>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PriceAfter = table.Column<double>(nullable: false),
                    Discount = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    categoryBook = table.Column<int>(nullable: false),
                    Copies = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jornals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    ISBN = table.Column<Guid>(nullable: false),
                    Publisher = table.Column<string>(nullable: true),
                    PrintDate = table.Column<DateTime>(nullable: false),
                    CopyNumber = table.Column<int>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PriceAfter = table.Column<double>(nullable: false),
                    Discount = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Month = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jornals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Jornals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
