using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class setor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteProspHistorico_ClienteProsp_ClienteProspId",
                table: "ClienteProspHistorico");

            migrationBuilder.AddColumn<int>(
                name: "SetorId",
                table: "ClienteProsp",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    SetorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.SetorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProsp_SetorId",
                table: "ClienteProsp",
                column: "SetorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteProsp_Setor_SetorId",
                table: "ClienteProsp",
                column: "SetorId",
                principalTable: "Setor",
                principalColumn: "SetorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteProspHistorico_ClienteProsp_ClienteProspId",
                table: "ClienteProspHistorico",
                column: "ClienteProspId",
                principalTable: "ClienteProsp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteProsp_Setor_SetorId",
                table: "ClienteProsp");

            migrationBuilder.DropForeignKey(
                name: "FK_ClienteProspHistorico_ClienteProsp_ClienteProspId",
                table: "ClienteProspHistorico");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropIndex(
                name: "IX_ClienteProsp_SetorId",
                table: "ClienteProsp");

            migrationBuilder.DropColumn(
                name: "SetorId",
                table: "ClienteProsp");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteProspHistorico_ClienteProsp_ClienteProspId",
                table: "ClienteProspHistorico",
                column: "ClienteProspId",
                principalTable: "ClienteProsp",
                principalColumn: "Id");
        }
    }
}
