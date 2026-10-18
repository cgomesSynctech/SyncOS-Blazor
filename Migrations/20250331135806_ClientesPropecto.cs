using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class ClientesPropecto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteProspStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteProspStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClienteProsp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevendaId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Razao = table.Column<string>(type: "varchar(255)", nullable: true),
                    Fantasia = table.Column<string>(type: "varchar(50)", nullable: true),
                    CPF_CNPJ = table.Column<string>(type: "varchar(14)", nullable: true),
                    Endereco = table.Column<string>(type: "varchar(255)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(255)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: true),
                    UFId = table.Column<string>(type: "Char(2)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true),
                    ValorNegociacao = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ValorManutencaoNegociacao = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    InscricaoEstadual = table.Column<string>(type: "varchar(20)", nullable: true),
                    Contato = table.Column<string>(type: "varchar(255)", nullable: true),
                    WhatsApp = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    Vendedor = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteProsp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteProsp_ClienteProspStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ClienteProspStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClienteProsp_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClienteProsp_Revendas_RevendaId",
                        column: x => x.RevendaId,
                        principalTable: "Revendas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClienteProsp_UFs_UFId",
                        column: x => x.UFId,
                        principalTable: "UFs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClienteProspHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteProspId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "Date", nullable: false),
                    Historico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arquivo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteProspHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteProspHistorico_ClienteProsp_ClienteProspId",
                        column: x => x.ClienteProspId,
                        principalTable: "ClienteProsp",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClienteProspHistorico_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProsp_MunicipioId",
                table: "ClienteProsp",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProsp_RevendaId",
                table: "ClienteProsp",
                column: "RevendaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProsp_StatusId",
                table: "ClienteProsp",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProsp_UFId",
                table: "ClienteProsp",
                column: "UFId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProspHistorico_ClienteProspId",
                table: "ClienteProspHistorico",
                column: "ClienteProspId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProspHistorico_UsuarioId",
                table: "ClienteProspHistorico",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteProspHistorico");

            migrationBuilder.DropTable(
                name: "ClienteProsp");

            migrationBuilder.DropTable(
                name: "ClienteProspStatus");
        }
    }
}
