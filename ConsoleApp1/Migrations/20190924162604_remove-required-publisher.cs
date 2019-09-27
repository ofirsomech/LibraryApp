using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApi.Migrations
{
    public partial class removerequiredpublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Jornals",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Books",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Jornals",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
