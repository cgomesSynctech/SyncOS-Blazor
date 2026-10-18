using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class ValoresComissoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValidateC",
                table: "Clientes",
                type: "Char(1)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorComissao",
                table: "Clientes",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorSped",
                table: "Clientes",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Vendedor",
                table: "Clientes",
                type: "varchar(30)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidateC",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ValorComissao",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ValorSped",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Vendedor",
                table: "Clientes");
        }
    }
}
