using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class HistoricoDesenv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesenvCardHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesenvCardId = table.Column<int>(type: "int", nullable: false),
                    DesenvCardStatusId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detalhes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solucao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resolucao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersaoSuapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arquivosolucao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenvCardHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesenvCardHistorico_DesenvCard_DesenvCardId",
                        column: x => x.DesenvCardId,
                        principalTable: "DesenvCard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DesenvCardHistorico_DesenvCardStatus_DesenvCardStatusId",
                        column: x => x.DesenvCardStatusId,
                        principalTable: "DesenvCardStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DesenvCardHistorico_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesenvCardHistorico_DesenvCardId",
                table: "DesenvCardHistorico",
                column: "DesenvCardId");

            migrationBuilder.CreateIndex(
                name: "IX_DesenvCardHistorico_DesenvCardStatusId",
                table: "DesenvCardHistorico",
                column: "DesenvCardStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DesenvCardHistorico_UsuarioId",
                table: "DesenvCardHistorico",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenvCardHistorico");
        }
    }
}
