using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADVO.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EleccionId",
                table: "Candidato",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartidoPoliticoId",
                table: "AlianzasPoliticas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_EleccionId",
                table: "Candidato",
                column: "EleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_AlianzasPoliticas_PartidoPoliticoId",
                table: "AlianzasPoliticas",
                column: "PartidoPoliticoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPolitico_PartidoPoliticoId",
                table: "AlianzasPoliticas",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPolitico",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidato_Eleccion_EleccionId",
                table: "Candidato",
                column: "EleccionId",
                principalTable: "Eleccion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPolitico_PartidoPoliticoId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidato_Eleccion_EleccionId",
                table: "Candidato");

            migrationBuilder.DropIndex(
                name: "IX_Candidato_EleccionId",
                table: "Candidato");

            migrationBuilder.DropIndex(
                name: "IX_AlianzasPoliticas_PartidoPoliticoId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropColumn(
                name: "EleccionId",
                table: "Candidato");

            migrationBuilder.DropColumn(
                name: "PartidoPoliticoId",
                table: "AlianzasPoliticas");
        }
    }
}
