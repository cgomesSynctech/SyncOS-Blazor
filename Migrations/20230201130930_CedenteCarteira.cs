using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncOSCore.Migrations
{
    public partial class CedenteCarteira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "ChamadoStatus",
            //    keyColumn: "Id",
            //    keyValue: 0);

            //migrationBuilder.DeleteData(
            //    table: "ChamadoStatus",
            //    keyColumn: "Id",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "ChamadoStatus",
            //    keyColumn: "Id",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    table: "ChamadoStatus",
            //    keyColumn: "Id",
            //    keyValue: 3);

            //migrationBuilder.DeleteData(
            //    table: "ChamadoStatus",
            //    keyColumn: "Id",
            //    keyValue: 6);

            //migrationBuilder.DeleteData(
            //    table: "ChamadoStatus",
            //    keyColumn: "Id",
            //    keyValue: 7);

            //migrationBuilder.DeleteData(
            //    table: "ChamadoStatus",
            //    keyColumn: "Id",
            //    keyValue: 100);

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Carteiras",
                newName: "Convenio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Convenio",
                table: "Carteiras",
                newName: "Codigo");

            //migrationBuilder.InsertData(
            //    table: "ChamadoStatus",
            //    columns: new[] { "Id", "Descricao" },
            //    values: new object[,]
            //    {
            //        { 0, "Finalizado" },
            //        { 1, "Aguardando Atendimento" },
            //        { 2, "Setor de Desenvolvimento" },
            //        { 3, "Em Análise" },
            //        { 6, "Em Atendimento" },
            //        { 7, "Aguardando Retorno Cliente" },
            //        { 100, "Todos" }
            //    });
        }
    }
}
