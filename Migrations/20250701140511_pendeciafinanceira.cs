using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class pendeciafinanceira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioResposta",
                table: "DesenvCardHistorico",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Pendencia",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PendenciaObs",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioResposta",
                table: "DesenvCardHistorico");

            migrationBuilder.DropColumn(
                name: "Pendencia",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PendenciaObs",
                table: "Clientes");
        }
    }
}
