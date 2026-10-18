using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class Clientemodulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Backup",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Certificado",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EspecieOutros",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MDF",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Nf",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Nfc",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Outros",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Retaguarda",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sped",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TEF",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VipShop",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backup",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Certificado",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EspecieOutros",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "MDF",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Nf",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Nfc",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Outros",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Retaguarda",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Sped",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TEF",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "VipShop",
                table: "Clientes");
        }
    }
}
