using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class DesenvArquivos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Arquivo",
                table: "DesenvCard",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Arquivosolucao",
                table: "DesenvCard",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkBase",
                table: "DesenvCard",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arquivo",
                table: "DesenvCard");

            migrationBuilder.DropColumn(
                name: "Arquivosolucao",
                table: "DesenvCard");

            migrationBuilder.DropColumn(
                name: "LinkBase",
                table: "DesenvCard");
        }
    }
}
