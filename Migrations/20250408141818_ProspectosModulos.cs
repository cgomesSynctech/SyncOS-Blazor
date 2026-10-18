using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class ProspectosModulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Backup",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Certificado",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EspecieOutros",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MDF",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Nf",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Nfc",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Outros",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Retaguarda",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sped",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TEF",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VipShop",
                table: "ClienteProsp",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backup",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "Certificado",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "EspecieOutros",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "MDF",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "Nf",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "Nfc",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "Outros",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "Retaguarda",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "Sped",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "TEF",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "VipShop",
                table: "ClienteProsp");
        }
    }
}
