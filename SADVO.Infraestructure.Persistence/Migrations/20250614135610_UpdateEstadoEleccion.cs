using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADVO.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEstadoEleccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Eleccion");

            migrationBuilder.AddColumn<bool>(
                name: "EstaFinalizada",
                table: "Eleccion",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaFinalizada",
                table: "Eleccion");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Eleccion",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "En Proceso");
        }
    }
}
