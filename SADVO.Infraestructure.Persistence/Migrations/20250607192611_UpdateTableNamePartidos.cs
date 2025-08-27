using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADVO.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableNamePartidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPPolitico_PartidoDestinoId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPPolitico_PartidoSolicitanteId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionPuesto_PartidoPPolitico_PartidoPoliticoId",
                table: "AsignacionPuesto");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidato_PartidoPPolitico_PartidoPoliticoId",
                table: "Candidato");

            migrationBuilder.DropForeignKey(
                name: "FK_DirigentePolitico_PartidoPPolitico_PartidoPoliticoId",
                table: "DirigentePolitico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartidoPPolitico",
                table: "PartidoPPolitico");

            migrationBuilder.RenameTable(
                name: "PartidoPPolitico",
                newName: "PartidoPolitico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartidoPolitico",
                table: "PartidoPolitico",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPolitico_PartidoDestinoId",
                table: "AlianzasPoliticas",
                column: "PartidoDestinoId",
                principalTable: "PartidoPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPolitico_PartidoSolicitanteId",
                table: "AlianzasPoliticas",
                column: "PartidoSolicitanteId",
                principalTable: "PartidoPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionPuesto_PartidoPolitico_PartidoPoliticoId",
                table: "AsignacionPuesto",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidato_PartidoPolitico_PartidoPoliticoId",
                table: "Candidato",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirigentePolitico_PartidoPolitico_PartidoPoliticoId",
                table: "DirigentePolitico",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPolitico_PartidoDestinoId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPolitico_PartidoSolicitanteId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionPuesto_PartidoPolitico_PartidoPoliticoId",
                table: "AsignacionPuesto");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidato_PartidoPolitico_PartidoPoliticoId",
                table: "Candidato");

            migrationBuilder.DropForeignKey(
                name: "FK_DirigentePolitico_PartidoPolitico_PartidoPoliticoId",
                table: "DirigentePolitico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartidoPolitico",
                table: "PartidoPolitico");

            migrationBuilder.RenameTable(
                name: "PartidoPolitico",
                newName: "PartidoPPolitico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartidoPPolitico",
                table: "PartidoPPolitico",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPPolitico_PartidoDestinoId",
                table: "AlianzasPoliticas",
                column: "PartidoDestinoId",
                principalTable: "PartidoPPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlianzasPoliticas_PartidoPPolitico_PartidoSolicitanteId",
                table: "AlianzasPoliticas",
                column: "PartidoSolicitanteId",
                principalTable: "PartidoPPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionPuesto_PartidoPPolitico_PartidoPoliticoId",
                table: "AsignacionPuesto",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidato_PartidoPPolitico_PartidoPoliticoId",
                table: "Candidato",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirigentePolitico_PartidoPPolitico_PartidoPoliticoId",
                table: "DirigentePolitico",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
