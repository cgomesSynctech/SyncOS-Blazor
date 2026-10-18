using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class ClienteCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "DesenvCard",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteNome",
                table: "DesenvCard",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "DesenvCard");

            migrationBuilder.DropColumn(
                name: "ClienteNome",
                table: "DesenvCard");
        }
    }
}
