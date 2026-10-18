using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class Historicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteHistoricos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "Date", nullable: false),
                    Historico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteHistoricos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteHistoricos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClienteHistoricos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteHistoricos_ClienteId",
                table: "ClienteHistoricos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteHistoricos_UsuarioId",
                table: "ClienteHistoricos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteHistoricos");
        }
    }
}
