using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class Desenv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValorNegociacao",
                table: "ClienteProsp",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorManutencaoNegociacao",
                table: "ClienteProsp",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.CreateTable(
                name: "DesenvCardStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenvCardStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesenvCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChamadoModuloId = table.Column<int>(type: "int", nullable: false),
                    DesenvCardStatusId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detalhes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solucao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resolucao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersaoSuapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenvCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesenvCard_ChamadoModulo_ChamadoModuloId",
                        column: x => x.ChamadoModuloId,
                        principalTable: "ChamadoModulo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DesenvCard_DesenvCardStatus_DesenvCardStatusId",
                        column: x => x.DesenvCardStatusId,
                        principalTable: "DesenvCardStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DesenvCard_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesenvCard_ChamadoModuloId",
                table: "DesenvCard",
                column: "ChamadoModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_DesenvCard_DesenvCardStatusId",
                table: "DesenvCard",
                column: "DesenvCardStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DesenvCard_UsuarioId",
                table: "DesenvCard",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenvCard");

            migrationBuilder.DropTable(
                name: "DesenvCardStatus");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorNegociacao",
                table: "ClienteProsp",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorManutencaoNegociacao",
                table: "ClienteProsp",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);
        }
    }
}
